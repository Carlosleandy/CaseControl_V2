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
    public class ReceptionMediumController : ControllerBase
    {
        private readonly IReceptionMedium _medium;

        public ReceptionMediumController(IReceptionMedium medium)
        {
            _medium = medium;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReceptionMediumAsync([FromQuery] PaginationDTO pagination)
        {
            try
            {
                return Ok(await _medium.GetAllReceptionMediumAsync(pagination));
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
                return Ok(await _medium.GetTotalPagesAsync(pagination));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<ReceptionMedium>> GetReceptionMediumAsync(int id)
        {
            try
            {
                var status = await _medium.GetReceptionMediumByIDAsync(id);
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
        public async Task<IActionResult> PutReceptionMediumAsync(ReceptionMedium model)
        {
            try
            {
                return Ok(await _medium.EditReceptionMediumAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un Medio de Recepción con este nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<ReceptionMedium>> PostReceptionMediumAsync(ReceptionMedium model)
        {
            try
            {
                return Ok(await _medium.AddReceptionMediumAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un Medio de Recepción con este nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteReceptionMedium(int id)
        {
            var exists = _medium.ReceptionMediumExists(id);
            if (!exists)
            {
                return NotFound();
            }

            await _medium.DeleteReceptionMediumAsync(id);

            return NoContent();
        }
    }
}
