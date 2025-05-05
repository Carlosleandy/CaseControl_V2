using CaseControl.Api.Interfaces;
using CaseControl.Api.TOKEN.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CaseControl.Api.Controllers
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
        public AccountController(IUser usersService, IToken token)
        {
            _usersService = usersService;
            _token = token;
        }

        [HttpGet("authenticate/{key}/{username}")]
        public async Task<IActionResult> ExistsKeySession(string key, string username)
        {
            try
            {
                //Recibe los datos del key y el Usuario.
                var user = await _usersService.AuthenticateUser(key, username);

                //Si la key o el Usuario no son validos, retorna lo siguiente:
                if (user == null) return Unauthorized("Key or user does not validate.");

                //Generar el token
                var token = _token.GenerateJwtToken(user);

                //Retorna el token si el usuario esta validado o autorizado.
                return StatusCode(StatusCodes.Status200OK, new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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


    }
}
