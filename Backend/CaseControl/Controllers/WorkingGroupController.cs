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
    public class WorkingGroupController : ControllerBase
    {
        private readonly IWorkingGroup _type;

        public WorkingGroupController(IWorkingGroup type)
        {
            _type = type;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkingGroupAsync([FromQuery] PaginationDTO pagination)
        {
            try
            {
                return Ok(await _type.GetAllWorkingGroupAsync(pagination));
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
        public async Task<ActionResult<WorkingGroup>> GetWorkingGroupAsync(int id)
        {
            try
            {
                var status = await _type.GetWorkingGroupByIDAsync(id);
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
        public async Task<IActionResult> PutWorkingGroupAsync(WorkingGroup model)
        {
            try
            {
                return Ok(await _type.EditWorkingGroupAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un Grupo de Trabajo con este nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<WorkingGroup>> PostWorkingGroupAsync(WorkingGroup model)
        {
            try
            {
                return Ok(await _type.AddWorkingGroupAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un Grupo de Trabajo con este nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteWorkingGroup(int id)
        {
            var exists = _type.WorkingGroupExists(id);
            if (!exists)
            {
                return NotFound();
            }

            await _type.DeleteWorkingGroupAsync(id);

            return NoContent();
        }
    }
}
