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
    public class FaultLinkedController : ControllerBase
    {
        private readonly IFaultLinked _faultlinked;

        public FaultLinkedController(IFaultLinked faultLinked)
        {
            _faultlinked = faultLinked;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFaultLinkedAsync([FromQuery] PaginationDTO pagination)
        {
            try
            {
                return Ok(await _faultlinked.GetAllFaultLinkedAsync(pagination));
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
                return Ok(await _faultlinked.GetTotalPagesAsync(pagination));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<FaultLinked>> GetFaultLinkedByLinkedAsync(int id)
        {
            try
            {
                var status = await _faultlinked.GetFaultLinkedByLinkedAsync(id);
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

        [HttpGet("GetFaultLinkedsByLinkedCode/{linkedID}")]
        public async Task<IActionResult> GetFaultLinkedsByLinkedCodeAsync(string linkedID)
        {
            try
            {
                var faults = await _faultlinked.GetFaultLinkedsByLinkedCodeAsync(linkedID);
                if (faults == null)
                {
                    return NotFound();
                }

                return Ok(faults);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<FaultLinked>> PostFaultLinkedAsync(FaultLinked model)
        {
            try
            {
                return Ok(await _faultlinked.AddFaultLinkedAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFaultLinkedAsync(int id)
        {
            var exists = _faultlinked.FaultLinkedExists(id);
            if (!exists)
            {
                return NotFound();
            }

            await _faultlinked.DeleteFaultLinkedAsync(id);

            return NoContent();
        }


        [HttpGet("generatePDFFaultsByLinkedCode/{code}")]
        public async Task<IActionResult> GeneratePDFFaultsByLinkedCodeAsync(string code)
        {
            try
            {
                var pdfContent = await _faultlinked.GeneratePDFFaultsByLinkedCodeAsync(code);
                return pdfContent == null ? NotFound() : File(pdfContent, "application/pdf", $"Faltas del Vinculado {code}_{DateTime.Now.ToString("yyyyMMdd")}.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
