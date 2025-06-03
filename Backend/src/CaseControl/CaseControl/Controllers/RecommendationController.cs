using CaseControl.Api.Interfaces;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace CaseControl.Api.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly IRecommendation _recommendation;

        public RecommendationController(IRecommendation recommendation)
        {
            _recommendation = recommendation;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecommendationAsync([FromQuery] PaginationDTO pagination)
        {
            try
            {
                return Ok(await _recommendation.GetAllRecommendationAsync(pagination));
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
                return Ok(await _recommendation.GetTotalPagesAsync(pagination));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<Recommendation>> GetRecommendationAsync(int id)
        {
            try
            {
                var status = await _recommendation.GetRecommendationByIDAsync(id);
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
        public async Task<IActionResult> PutRecommendationAsync(Recommendation model)
        {
            try
            {


                return Ok(await _recommendation.EditRecommendationAsync(model));
            }
           
             catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<Recommendation>> PostRecommendationAsync(Recommendation model)
        {
            try
            {


                return Ok(await _recommendation.AddRecommendationAsync(model));
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRecommendation(int id)
        {
            var exists = _recommendation.RecommendationExists(id);
            if (!exists)
            {
                return NotFound();
            }

            await _recommendation.DeleteRecommendationAsync(id);

            return NoContent();
        }


        [HttpGet("getcasesbyrecommendationtype/{id:int}")]
        public async Task<IActionResult> GetCasesByRecommendationTypeAsync(int id)
        {
            try
            {
                return Ok(await _recommendation.GetCasesByRecommendationTypeAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getcasesbyrecommendationtypesummary")]
        public async Task<IActionResult> GetCasesByRecommendationTypeSummaryAsync()
        {
            try
            {
                return Ok(await _recommendation.GetCasesByRecommendationTypeSummaryAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("generatePDFcasesbyrecommendationtypesummary")]
        public async Task<IActionResult> GeneratePDFCasesByRecommendationTypeSummaryAsync()
        {
            try
            {
                var pdfContent = await _recommendation.GeneratePDFCasesByRecommendationTypeSummaryAsync();
                return File(pdfContent, "application/pdf", $"Resúmen Casos por Tipo de Recomendación {DateTime.Now.ToString("yyyyMMdd")}.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("generatePDFcasesbyrecommendationtype/{id:int}")]
        public async Task<IActionResult> GeneratePDFCasesByRecommendationTypeAsync(int id)
        {
            try
            {
                var pdfContent = await _recommendation.GeneratePDFCasesByRecommendationTypeAsync(id);
                return pdfContent == null ? NotFound() : File(pdfContent, "application/pdf", $"Resúmen Casos por Tipo de Recomendación {DateTime.Now.ToString("yyyyMMdd")}.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
