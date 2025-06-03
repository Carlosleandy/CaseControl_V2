
using CaseControl.Api.Interfaces;
using CaseControl.Api.TOKEN.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CaseControl.Api.Helpers;

namespace CaseControl.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors("CorsPolicy")]
    [ApiController]
    //[Route("/security/session_exists/")]
    [Produces("application/json")] //Para que el contenido sea siempre Tipo JSON
    public class AccountController : ControllerBase
    {
        private readonly IUser _usersService;
        private readonly IToken _token;
        private readonly UtilService _utilidades;
        public AccountController(IUser usersService, IToken token, UtilService utilidades)
        {
            _usersService = usersService;
            _token = token;
            _utilidades = utilidades;
        }


        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            var us = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _usersService.GetUserByUserNameAsync(us);
            var tokenRefresh = await _token.GenerateJwtToken(user);
            //var tokenRefresh = await _token.RegenerateJwtToken(token);

            if (tokenRefresh != null)
            {
                return StatusCode(StatusCodes.Status200OK, new { Token = tokenRefresh });
            }
            else
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
        }
    [HttpPost]
    [Route("Login2")]
    public async Task<IActionResult> Login(LoginDTO objeto)
    {
       try
       {
           // Validación básica de entrada
           if (string.IsNullOrEmpty(objeto.UserName) || string.IsNullOrEmpty(objeto.Password))
           {
               return BadRequest(new { isSuccess = false, message = "El nombre de usuario y la contraseña son obligatorios." });
           }

           var usuarioEncontrado = await _usersService.GetUserByUserNameAsync(objeto.UserName);
           if (usuarioEncontrado == null || usuarioEncontrado.PasswordHash != _utilidades.encriptarSHA256(objeto.Password))
           {
               return Unauthorized(new { isSuccess = false, message = "Credenciales incorrectas." });
           }

           else
               return Ok(new { isSuccess = true, token = _token.GenerateJwtToken(usuarioEncontrado) });
       }
       catch (Exception ex)
       {
           // Log del error
           Console.WriteLine($"Error en login: {ex.Message}");
           return StatusCode(StatusCodes.Status500InternalServerError, new { isSuccess = false, message = "Error interno del servidor." });
       }
    }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginTDO objeto)
        {
            try
            {
                // Validación básica de entrada
                if (string.IsNullOrEmpty(objeto.UserName) || string.IsNullOrEmpty(objeto.Password))
                {
                    return BadRequest(new { isSuccess = false, message = "El nombre de usuario y la contraseña son obligatorios." });
                }

                // Usar el método Authenticate existente
                var user = await _usersService.Authenticate(objeto.UserName, objeto.Password);

                if (user == null)
                {
                    // Usar código 401 para credenciales incorrectas
                    return Unauthorized(new { isSuccess = false, message = "Credenciales incorrectas." });
                }

                // Generar token JWT
                var token = await _token.GenerateJwtToken(user);

                // Devolver respuesta exitosa con más información
                return Ok(new { 
                    isSuccess = true, 
                    token,
                    user = new {
                        id = user.ID,
                        userName = user.UserName,
                        isAdmin = user.IsAdmin,
                        isActive = user.IsActive
                    }
                });
            }
            catch (Exception ex)
            {
                // Log del error con más detalles
                Console.WriteLine($"Error en login: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"InnerException: {ex.InnerException.Message}");
                }
                
                // En ambiente de desarrollo, devolver detalles del error
                return StatusCode(StatusCodes.Status500InternalServerError, new { 
                    isSuccess = false, 
                    message = "Error interno del servidor.", 
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }
    
    }
}