using CaseControl.Api.Interfaces;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaseControl.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewController : ControllerBase
    {
        private readonly IInterview _interview;

        public InterviewController(IInterview type)
        {
            _interview = type;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInterviewAsync([FromQuery] PaginationDTO pagination)
        {
            try
            {
                return Ok(await _interview.GetAllInterviewAsync(pagination));
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
                return Ok(await _interview.GetTotalPagesAsync(pagination));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<Interview>> GetInterviewAsync(int id)
        {
            try
            {
                var status = await _interview.GetInterviewByIDAsync(id);
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
        public async Task<IActionResult> PutInterviewAsync(Interview model)
        {
            try
            {
                return Ok(await _interview.EditInterviewAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una Entrevista con esta descripción.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<Interview>> PostInterviewAsync(Interview model)
        {
            try
            {
                return Ok(await _interview.AddInterviewAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una Entrevista con esta descripción.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteInterviewAsync(int id)
        {
            var exists = _interview.InterviewExists(id);
            if (!exists)
            {
                return NotFound();
            }

            await _interview.DeleteInterviewAsync(id);

            return NoContent();
        }


        [HttpGet("getallintervieweetype")]
        public async Task<IActionResult> GetAllIntervieweeTypeAsync()
        {
            try
            {
                return Ok(await _interview.GetAllIntervieweeTypesAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
