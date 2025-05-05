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
    public class RecommendationStatusController : ControllerBase
    {
        private readonly IRecommendationStatus _status;

        public RecommendationStatusController(IRecommendationStatus status)
        {
            _status = status;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecommendationStatusAsync([FromQuery] PaginationDTO pagination)
        {
            try
            {
                return Ok(await _status.GetAllRecommendationStatusAsync(pagination));
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
                return Ok(await _status.GetTotalPagesAsync(pagination));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<RecommendationStatus>> GetRecommendationStatusAsync(int id)
        {
            try
            {
                var status = await _status.GetRecommendationStatusByIDAsync(id);
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
        public async Task<IActionResult> PutRecommendationStatusAsync(RecommendationStatus model)
        {
            try
            {
                return Ok(await _status.EditRecommendationStatusAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un Estado con este nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<RecommendationStatus>> PostRecommendationStatusAsync(RecommendationStatus model)
        {
            try
            {
                return Ok(await _status.AddRecommendationStatusAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un Estado con este nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRecommendationStatus(int id)
        {
            var exists = _status.RecommendationStatusExists(id);
            if (!exists)
            {
                return NotFound();
            }

            await _status.DeleteRecommendationStatusAsync(id);

            return NoContent();
        }
    }
}
