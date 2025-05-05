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
    public class EvidenceClassificationController : ControllerBase
    {
        private readonly IEvidenceClassification _classification;

        public EvidenceClassificationController(IEvidenceClassification type)
        {
            _classification = type;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvidenceClassificationAsync([FromQuery] PaginationDTO pagination)
        {
            try
            {
                return Ok(await _classification.GetAllEvidenceClassificationAsync(pagination));
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
                return Ok(await _classification.GetTotalPagesAsync(pagination));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<EvidenceClassification>> GetEvidenceClassificationAsync(int id)
        {
            try
            {
                var status = await _classification.GetEvidenceClassificationByIDAsync(id);
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
        public async Task<IActionResult> PutEvidenceClassificationAsync(EvidenceClassification model)
        {
            try
            {
                return Ok(await _classification.EditEvidenceClassificationAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una Bitácora con esta descripción.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<EvidenceClassification>> PostEvidenceClassificationAsync(EvidenceClassification model)
        {
            try
            {
                return Ok(await _classification.AddEvidenceClassificationAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una Bitácora con esta descripción.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEvidenceClassificationAsync(int id)
        {
            var exists = _classification.EvidenceClassificationExists(id);
            if (!exists)
            {
                return NotFound();
            }

            await _classification.DeleteEvidenceClassificationAsync(id);

            return NoContent();
        }
    }
}
