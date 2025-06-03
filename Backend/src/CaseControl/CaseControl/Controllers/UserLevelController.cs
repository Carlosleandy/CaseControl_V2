using CaseControl.Api.Interfaces;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaseControl.Api.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserLevelController : ControllerBase
    {
        private readonly IUserLevel _type;

        public UserLevelController(IUserLevel type)
        {
            _type = type;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserLevelAsync([FromQuery] PaginationDTO pagination)
        {
            try
            {
                return Ok(await _type.GetAllUserLevelAsync(pagination));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            try
            {
                return Ok(await _type.GetTotalPagesAsync(pagination));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserLevel>> GetUserLevelAsync(int id)
        {
            try
            {
                var status = await _type.GetUserLevelByIDAsync(id);
                if (status == null)
                {
                    return NotFound();
                }

                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> PutUserLevelAsync(UserLevel model)
        {
            try
            {
                return Ok(await _type.EditUserLevelAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un Tipo de Caso con este nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<UserLevel>> PostUserLevelAsync(UserLevel model)
        {
            try
            {
                return Ok(await _type.AddUserLevelAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un Tipo de Caso con este nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUserLevel(int id)
        {
            var exists = _type.UserLevelExists(id);
            if (!exists)
            {
                return NotFound();
            }

            await _type.DeleteUserLevelAsync(id);

            return NoContent();
        }
    }
}
