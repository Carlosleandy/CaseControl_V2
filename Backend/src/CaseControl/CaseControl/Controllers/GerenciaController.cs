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
    public class GerenciaController : ControllerBase
    {
        private readonly IGerencia _type;

        public GerenciaController(IGerencia type)
        {
            _type = type;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGerenciaAsync([FromQuery] PaginationDTO pagination)
        {
            try
            {
                return Ok(await _type.GetAllGerenciaAsync(pagination));
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
        public async Task<ActionResult<Gerencia>> GetGerenciaAsync(int id)
        {
            try
            {
                var status = await _type.GetGerenciaByIDAsync(id);
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
        public async Task<IActionResult> PutGerenciaAsync(Gerencia model)
        {
            try
            {
                return Ok(await _type.EditGerenciaAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una Gerencia con este nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<Gerencia>> PostGerenciaAsync(Gerencia model)
        {
            try
            {
                return Ok(await _type.AddGerenciaAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una Gerencia con este nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGerencia(int id)
        {
            var exists = _type.GerenciaExists(id);
            if (!exists)
            {
                return NotFound();
            }

            await _type.DeleteGerenciaAsync(id);

            return NoContent();
        }
    }
}
