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
    public class RecommendationTypeController : ControllerBase
    {
        private readonly IRecommendationType _type;

        public RecommendationTypeController(IRecommendationType type)
        {
            _type = type;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecommendationTypeAsync([FromQuery] PaginationDTO pagination)
        {
            try
            {
                return Ok(await _type.GetAllRecommendationTypeAsync(pagination));
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
        public async Task<ActionResult<RecommendationType>> GetRecommendationTypeAsync(int id)
        {
            try
            {
                var type = await _type.GetRecommendationTypeByIDAsync(id);
                if (type == null)
                {
                    return NotFound();
                }

                return Ok(type);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> PutRecommendationTypeAsync(RecommendationType model)
        {
            try
            {
                return Ok(await _type.EditRecommendationTypeAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un Tipo de Recomendación con este nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<RecommendationType>> PostRecommendationTypeAsync(RecommendationType model)
        {
            try
            {
                return Ok(await _type.AddRecommendationTypeAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un Tipo de Recomendación con este nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRecommendationType(int id)
        {
            var exists = _type.RecommendationTypeExists(id);
            if (!exists)
            {
                return NotFound();
            }

            await _type.DeleteRecommendationTypeAsync(id);

            return NoContent();
        }
    }
}
