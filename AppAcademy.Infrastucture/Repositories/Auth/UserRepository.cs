using AppAcademy.Application.Contracts.Persistence.Auth;
using AppAcademy.Application.DTOs;
using AppAcademy.Application.Features.Auth.Users.Commands.CreateUser;
using AppAcademy.Application.Features.Auth.Users.Commands.LoginUser;
using AppAcademy.Domain.Auth;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AppAcademy.Infrastucture.Repositories.Auth
{
    public class UserRepository : AsyncRepository<User>, IUserRepository
    {
        private readonly SymmetricSecurityKey _key;
        private readonly IConfiguration _configuration;

        public UserRepository(AppAcademyDbContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        }

        #region CrearToken
        public string CrearToken(User user)
        {
            var claims = new List<Claim>()
           {
               new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
               //new Claim(ClaimTypes.Role, user.Rol.NameRol)
           };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(20),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion

        #region RegisterUser
        public async Task<UserDto> RegisterUser(CreateUserCommand createUserCommand)
        {
            if (await _dbContext.Users.AnyAsync(x => x.UserName == createUserCommand.UserName.ToLower()))
            {
                throw new InvalidOperationException("El Usuario ya existe");
            }

            using var hmac = new HMACSHA512();
            var user = new User
            {
                Email = createUserCommand.Email,
                UserName = createUserCommand.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(createUserCommand.Password)),
                PasswordSalt = hmac.Key,
                FechaCreacion = DateTime.Now
            };

            // Asignar Rol
            var rol = await _dbContext.Roles.SingleOrDefaultAsync(r => r.NameRol == createUserCommand.NameRol);
            if (rol != null)
            {
                user.Rol = rol;
            }

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            var refreshToken = await CreateRefreshToken(user);

            return new UserDto
            {
                UserName = createUserCommand.UserName,
                Token = CrearToken(user),
                RefreshToken = refreshToken.Token
            };
        }
        #endregion

        #region Login
        public async Task<UserDto> Login(LoginUserCommand loginUserCommand)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.UserName == loginUserCommand.UserName);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Usuario no existe");
            }
            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginUserCommand.Password));
            if (user == null || !computedHash.SequenceEqual(user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Usuario o contraseña no válidos");
            }

            var refreshToken = await CreateRefreshToken(user);

            return new UserDto
            {
                UserName = loginUserCommand.UserName,
                Token = CrearToken(user),
                RefreshToken = refreshToken.Token
            };
        }
        #endregion

        #region AssignRoleToUser
        public async Task AssignRoleToUser(string userId, string nameRol)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            var rol = await _dbContext.Roles.SingleOrDefaultAsync(r => r.NameRol == nameRol);
            if (rol == null)
            {
                throw new Exception("Role no encontrado");
            }

            user.Rol = rol;
            await _dbContext.SaveChangesAsync();
        }
        #endregion

        #region CreateRefreshToken
        public async Task<RefreshToken> CreateRefreshToken(User user)
        {
            var refreshToken = new RefreshToken
            {
                Token = GenerateRefreshToken(),
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                User = user
            };

            user.RefreshTokens.Add(refreshToken);
            await _dbContext.SaveChangesAsync();
            return refreshToken;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        #endregion

        #region RefreshTokenAsync
        public async Task<UserDto> RefreshTokenAsync(string refreshToken)
        {
            var storedToken = await _dbContext.RefreshTokens
                .Include(rt => rt.User)
                .SingleOrDefaultAsync(rt => rt.Token == refreshToken && !rt.IsRevoked);

            if (storedToken == null || storedToken.ExpiryDate < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Refresh token no válido o expirado");
            }

            var newJwtToken = CrearToken(storedToken.User);
            var newRefreshToken = await CreateRefreshToken(storedToken.User);

            // Marcar el token anterior como revocado
            storedToken.IsRevoked = true;
            storedToken.Revoked = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

            return new UserDto
            {
                UserName = storedToken.User.UserName,
                Token = newJwtToken,
                RefreshToken = newRefreshToken.Token
            };
        }
        #endregion
    }
}
