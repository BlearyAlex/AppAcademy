using AppAcademy.Application.DTOs;
using AppAcademy.Infrastucture.Identity;
using AppAcademy.Infrastucture.Repositories.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [Authorize(Roles = "Admin")]
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
            string roleName = string.IsNullOrEmpty(model.Role) ? "User" : model.Role;

            if (!await _roleManager.RoleExistsAsync(roleName))
                return BadRequest("El rol no existe.");

            await _userManager.AddToRoleAsync(user, roleName);

            return Ok(new { message = "Usuario registrado con éxito." });
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

        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            var userList = new List<object>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                userList.Add(new
                {
                    user.Id,
                    user.UserName,
                    user.Email,
                    user.FullName,
                    Rol = roles
                });
            }

            return Ok(userList);
        }

        [HttpPut("update-user/{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserModel model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("Usuario no encontrado.");

            user.UserName = model.UserName;
            user.FullName = model.FullName;
            user.Email = model.Email;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return BadRequest(updateResult.Errors);

            // Si se envia una nueva contrasena, intentamos cambiarla.
            if (!string.IsNullOrEmpty(model.Password))
            {
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await _userManager.ResetPasswordAsync(user, resetToken, model.Password);

                if (!passwordResult.Succeeded)
                    return BadRequest(passwordResult.Errors);
            }

            // Si envía un nuevo rol, lo actualizamos
            if (!string.IsNullOrEmpty(model.Role))
            {
                // Verificar si el rol existe
                if (!await _roleManager.RoleExistsAsync(model.Role))
                    return BadRequest("El rol especificado no existe.");

                // Obtener los roles actuales del usuario
                var currentRoles = await _userManager.GetRolesAsync(user);

                // Remover los roles anteriores y asignar el nuevo
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, model.Role);
            }

            return Ok(new { message = "Usuario actualizado con éxito." });
        }

        [HttpGet("get-user/{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            var userModel = new
            {
                user.Id,
                user.UserName,
                user.FullName,
                user.Email,
                Role = await GetUserRoleAsync(user),
            };

            return Ok(userModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-user/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var currentUserId = _userManager.GetUserId(User);

            if (userId == currentUserId)
            {
                return BadRequest("No puedes eliminar tu propio usuario.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            // Eliminar el usuario
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { message = "Usuario eliminado con éxito." });
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
                expires: DateTime.UtcNow.AddDays(7),
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
