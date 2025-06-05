// modificado por el Pasante Carlos Leandy Moreno Reyes (Alea: EL Varon)
using CaseControl.Api.Interfaces;
using CaseControl.Api.TOKEN.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CaseControl.Api.Helpers;

namespace CaseControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")] // Para que el contenido sea siempre Tipo JSON
    public class AccountController : ControllerBase
    {
        private readonly IUser _usersService;
        private readonly IToken _token;
        private readonly CaseControl.Api.Interfaces.IUtil _utilidades; // Especificar espacio de nombres

        public AccountController(IUser usersService, IToken token, CaseControl.Api.Interfaces.IUtil utilidades)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
            _token = token ?? throw new ArgumentNullException(nameof(token));
            _utilidades = utilidades ?? throw new ArgumentNullException(nameof(utilidades));
        }

        [HttpGet("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { isSuccess = false, message = "Usuario no autenticado." });
            }
            var user = await _usersService.GetUserByUserNameAsync(userId);
            if (user == null)
            {
                return NotFound(new { isSuccess = false, message = "Usuario no encontrado." });
            }
            var tokenRefresh = await _token.GenerateJwtToken(user);
            return tokenRefresh != null
                ? StatusCode(StatusCodes.Status200OK, new { Token = tokenRefresh })
                : StatusCode(StatusCodes.Status403Forbidden);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO objeto)
        {
            try
            {
                if (string.IsNullOrEmpty(objeto.UserName) || string.IsNullOrEmpty(objeto.Password))
                {
                    return BadRequest(new { isSuccess = false, message = "El nombre de usuario y la contraseña son obligatorios." });
                }

                var user = await _usersService.Authenticate(objeto.UserName, objeto.Password);

                if (user == null)
                {
                    return Unauthorized(new { isSuccess = false, message = "Credenciales incorrectas." });
                }

                var token = await _token.GenerateJwtToken(user);

                return Ok(new
                {
                    isSuccess = true,
                    token,
                    user = new
                    {
                        id = user.ID,
                        userName = user.UserName,
                        isAdmin = user.IsAdmin,
                        isActive = user.IsActive
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en login: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"InnerException: {ex.InnerException.Message}");
                }

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    isSuccess = false,
                    message = "Error interno del servidor.",
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }
    }
}