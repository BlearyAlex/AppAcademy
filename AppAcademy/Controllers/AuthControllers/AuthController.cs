using AppAcademy.Application.DTOs;
using AppAcademy.Infrastucture.Identity;
using AppAcademy.Infrastucture.Repositories.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppAcademy.Controllers.AuthControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RefreshTokenService _refreshTokenService;

        public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager, RefreshTokenService refreshTokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return BadRequest("El correo ya está en uso.");

            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Asignar el rol (si no lo manda, se pone "User" por defecto)
            string role = string.IsNullOrEmpty(model.Role) ? "User" : model.Role;

            if (!await _roleManager.RoleExistsAsync(role))
                return BadRequest("El rol no existe.");

            await _userManager.AddToRoleAsync(user, role);

            return Ok("Usuario registrado con éxito.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                // Buscar el usuario por su email
                var user = await _userManager.FindByEmailAsync(model.Email);

                // Verificar que el usuario exista y que la contraseña sea correcta
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    // Crear los claims del usuario (información que irá en el token)
                    var claims = new[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, await GetUserRoleAsync(user)) // Agregamos el rol al token
                };

                    // Generar una clave secreta
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("P3m7e0NbBTIWUdrv03TNxoHcBvqmXjdOODN7iMoEbeo"));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    // Crear el token JWT
                    var token = new JwtSecurityToken(
                        claims: claims,
                        expires: DateTime.UtcNow.AddHours(1),
                        signingCredentials: creds
                    );

                    // Generar el refresh token
                    var refreshToken = Guid.NewGuid().ToString();

                    // Guardar el refresh token en la base de datos
                    await _refreshTokenService.SaveRefreshToken(user, refreshToken);

                    // Devolver el token JWT
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        refreshToken = refreshToken
                    });
                }

                return Unauthorized("Invalid Credentials.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = _roleManager.Roles.Select(r => r.Name).ToList();
            return Ok(roles);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest model)
        {
            // Verificar si el refresh token existe y está asociado con un usuario
            var refreshToken = await _refreshTokenService.GetRefreshToken(model.RefreshToken);

            if (refreshToken == null || refreshToken.ExpiryDate <= DateTime.UtcNow)
            {
                return Unauthorized("Invalid or expired refresh token.");
            }

            // Obtener el usuario asociado con el refresh token
            var user = await _userManager.FindByIdAsync(refreshToken.UserId);

            if (user == null)
            {
                return Unauthorized("User not found.");
            }

            // Crear nuevos claims para el JWT
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, await GetUserRoleAsync(user)) // Agregamos el rol al token
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("P3m7e0NbBTIWUdrv03TNxoHcBvqmXjdOODN7iMoEbeo"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Crear el nuevo token JWT
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            // Eliminar el refresh token viejo (para evitar su reutilización)
            await _refreshTokenService.DeleteRefreshToken(refreshToken);

            // Generar un nuevo refresh token y guardarlo
            var newRefreshToken = Guid.NewGuid().ToString();
            await _refreshTokenService.SaveRefreshToken(user, newRefreshToken);

            // Devolver el nuevo JWT y refresh token
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = newRefreshToken
            });
        }

        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var user = HttpContext.User;
            var roleClaim = user.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            return Ok(new { User = user.Identity.Name, Role = roleClaim?.Value });
        }

        private async Task<string> GetUserRoleAsync(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault() ?? "User";  // Si no tiene rol, asignar "User" por defecto
        }
    }
}
