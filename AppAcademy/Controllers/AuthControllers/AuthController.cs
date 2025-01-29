using AppAcademy.Application.DTOs;
using AppAcademy.Infrastucture.Identity;
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

        public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
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
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("miClaveSecretaSuperSegura123"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Crear el token JWT
                var token = new JwtSecurityToken(
                    issuer: "",
                    audience: "",
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                );

                //// Crear el refresh token
                //var refreshToken = Guid.NewGuid().ToString();
                //await SaveRefreshToken(user, refreshToken); // Guardamos el refresh token en la base de datos

                // Devolver el token JWT
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                });
            }

            return Unauthorized("Invalid Credentials.");
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = _roleManager.Roles.Select(r => r.Name).ToList();
            return Ok(roles);
        }

        private async Task<string> GetUserRoleAsync(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault() ?? "User";  // Si no tiene rol, asignar "User" por defecto
        }
    }
}
