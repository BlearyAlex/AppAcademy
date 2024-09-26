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

        public string CrearToken(User user)
        {
            var claims = new List<Claim>()
           {
               new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
           };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

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

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return new UserDto
            {
                UserName = createUserCommand.UserName,
                Token = CrearToken(user),
            };
        }

        public async Task<UserDto> Login(LoginUserCommand loginUserCommand)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.UserName == loginUserCommand.UserName);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Usuario no existe");
            }
            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginUserCommand.Password));
            if (!computedHash.SequenceEqual(user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Password no válido");
            }

            return new UserDto
            {
                UserName = loginUserCommand.UserName,
                Token = CrearToken(user)
            };
        }
    }
}
