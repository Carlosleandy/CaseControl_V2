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
    public class CaseTypeController : ControllerBase
    {
        private readonly ICaseType _type;

        public CaseTypeController(ICaseType type)
        {
            _type = type;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCaseTypeAsync([FromQuery] PaginationDTO pagination)
        {
            try
            {
                return Ok(await _type.GetAllCaseTypeAsync(pagination));
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
        public async Task<ActionResult<CaseType>> GetCaseTypeAsync(int id)
        {
            try
            {
                var status = await _type.GetCaseTypeByIDAsync(id);
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
        public async Task<IActionResult> PutCaseTypeAsync(CaseType model)
        {
            try
            {
                return Ok(await _type.EditCaseTypeAsync(model));
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
        public async Task<ActionResult<CaseType>> PostCaseTypeAsync(CaseType model)
        {
            try
            {
                return Ok(await _type.AddCaseTypeAsync(model));
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
        public async Task<IActionResult> DeleteCaseType(int id)
        {
            var exists = _type.CaseTypeExists(id);
            if (!exists)
            {
                return NotFound();
            }

            await _type.DeleteCaseTypeAsync(id);

            return NoContent();
        }
    }
}
