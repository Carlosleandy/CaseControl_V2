using CaseControl.Api.Interfaces;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace CaseControl.Api.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CaseController : ControllerBase
    {
        private readonly ICase _case;
        public CaseController(ICase cas)
        {
            _case = cas;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCaseAsync([FromQuery] PaginationDTO pagination)
        {
            try
            {
                var result = await _case.GetAllCaseAsync(pagination);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getallcaseonly")]
        public async Task<IActionResult> GetAllCaseOnlyAsync([FromQuery] PaginationDTO pagination)
        {
            try
            {
                return Ok(await _case.GetAllCaseOnlyAsync(pagination));
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
                return Ok(await _case.GetTotalPagesAsync(pagination));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<Case>> GetCaseAsync(int id)
        {
            try
            {
                var status = await _case.GetCaseByIDAsync(id);
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
        public async Task<IActionResult> PutCaseAsync(Case model)
        {
            try
            {
                return Ok(await _case.EditCaseAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un Caso con este Asunto.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<Case>> PostCaseAsync(Case model)
        {
            try
            {
                return Ok(await _case.AddCaseAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un Caso con este Asunto.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("AddCaseAssignment")]
        public async Task<ActionResult<CaseAssignment>> AddCaseAssignmentAsync(CaseAssignment model)
        {
            try
            {
                return Ok(await _case.AddCaseAssignmentAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("AddCaseStatusChange")]
        public async Task<ActionResult<CaseStatusChange>> AddCaseStatusChangeAsync(CaseStatusChange model)
        {
            try
            {
                return Ok(await _case.AddCaseStatusChangeAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //[HttpDelete("{id:int}")]
        //public async Task<IActionResult> DeleteCase(int id)
        //{
        //    var exists = _case.CaseExists(id);
        //    if (!exists)
        //    {
        //        return NotFound();
        //    }

        //    await _case.DeleteCaseAsync(id);

        //    return NoContent();
        //}

        [HttpGet("getcasesstatuschangehist/{caseid:int}")]
        public async Task<IActionResult> GetCasesStatusChangeHistAsync(int caseid)
        {
            try
            {
                var changes = await _case.GetCasesStatusChangeHistAsync(caseid);
                if (changes == null)
                {
                    return NotFound();
                }

                return Ok(changes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getcasesamountrecovery/{caseid:int}")]
        public async Task<IActionResult> GetCasesAmountRecoveryAsync(int caseid)
        {
            try
            {
                var recoveries = await _case.GetCasesAmountRecoveryAsync(caseid);
                if (recoveries == null)
                {
                    return NotFound();
                }

                return Ok(recoveries);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("addcaseamountrecovery")]
        public async Task<ActionResult<Case>> AddCaseAmountRecoveryAsync(RecoveryHistory model)
        {
            try
            {
                return Ok(await _case.AddCaseAmountRecoveryAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetRankingCasesByUser")]
        public async Task<IActionResult> GetRankingCasesByUserAsync()
        {
            try
            {
                return Ok(await _case.GetRankingCasesByUserAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("getcasesrecoverysummary")]
        public async Task<IActionResult> GetCasesRecoverySummaryAsync()
        {
            try
            {
                return Ok(await _case.GetCasesRecoverySummaryAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("getcasesbystatus/{statusID:int}")]
        public async Task<IActionResult> GetCasesByStatusAsync(int statusID)
        {
            try
            {
                return Ok(await _case.GetCasesByStatusAsync(statusID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getcasesbystatussummary")]
        public async Task<IActionResult> GetCasesByStatusSummaryAsync()
        {
            try
            {
                return Ok(await _case.GetCasesByStatusSummaryAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("getcasesbyusername")]
        public async Task<IActionResult> GetCasesByUserNameAsync(string username)
        {
            try
            {
                return Ok(await _case.GetCasesByUserNameAsync(username));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getcasesbyusernamesummary")]
        public async Task<IActionResult> GetCasesByUserNameSummaryAsync()
        {
            try
            {
                return Ok(await _case.GetCasesByUserNameSummaryAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetCasesByCodeLinked/{code}")]
        public async Task<IActionResult> GetCasesByCodeLinkedAsync(string code)
        {
            try
            {
                return Ok(await _case.GetCasesByCodeLinkedAsync(code));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GeneratePDFRankingCasesByUser")]
        public async Task<IActionResult> GeneratePDFRankingCasesByUserAsync()
        {
            try
            {
                var pdfContent = await _case.GeneratePDFRankingCasesByUserAsync();
                return File(pdfContent, "application/pdf", $"Ranking de Casos por Usuario_{DateTime.Now.ToString("yyyyMMdd")}.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GeneratePDFCasesByLinkedCode/{code}")]
        public async Task<IActionResult> GeneratePDFCasesByLinkedCodeAsync(string code)
        {
            try
            {
                var pdfContent = await _case.GeneratePDFCasesByLinkedCodeAsync(code);
                return File(pdfContent, "application/pdf", $"Historial de Casos del Vinculado - {code}_{DateTime.Now.ToString("yyyyMMdd")}.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("generatePDFcasesstatuschangehist/{id:int}")]
        public async Task<IActionResult> GeneratePDFCasesStatusChangeHistAsync(int id)
        {
            try
            {
                var pdfContent = await _case.GeneratePDFCasesStatusChangeHistAsync(id);
                return File(pdfContent, "application/pdf", $"Historial de Cambios de Estado del Caso - {id}_{DateTime.Now.ToString("yyyyMMdd")}.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("generatePDFcasesbystatussummary")]
        public async Task<IActionResult> GeneratePDFCasesByUserNameSummaryAsync()
        {
            try
            {
                var pdfContent = await _case.GeneratePDFCasesByStatusSummaryAsync();
                return File(pdfContent, "application/pdf", $"Resúmen Casos por Estados {DateTime.Now.ToString("yyyyMMdd")}.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("generatePDFcasesbyusernamesummary")]
        public async Task<IActionResult> GeneratePDFCasesByStatusSummaryAsync()
        {
            try
            {
                var pdfContent = await _case.GeneratePDFCasesByUserNameSummaryAsync();
                return File(pdfContent, "application/pdf", $"Resúmen Casos por Usuarios {DateTime.Now.ToString("yyyyMMdd")}.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("generatePDFcasesbystatus/{id:int}")]
        public async Task<IActionResult> GeneratePDFCasesByStatusAsync(int id)
        {
            try
            {
                var pdfContent = await _case.GeneratePDFCasesByStatusAsync(id);
                return pdfContent == null ? NotFound() : File(pdfContent, "application/pdf", $"Casos en Estado {id}_{DateTime.Now.ToString("yyyyMMdd")}.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("generatePDFcasesbyusername")]
        public async Task<IActionResult> GeneratePDFCasesByUserNameAsync(string username)
        {
            try
            {
                var pdfContent = await _case.GeneratePDFCasesByUserNameAsync(username);
                return pdfContent == null ? NotFound() : File(pdfContent, "application/pdf", $"Casos del Empleado {username}_{DateTime.Now.ToString("yyyyMMdd")}.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("generatePDFcasesrecoverysummary")]
        public async Task<IActionResult> GeneratePDFCasesRecoverySummaryAsync()
        {
            try
            {
                var pdfContent = await _case.GeneratePDFCasesRecoverySummaryAsync();
                return pdfContent == null ? NotFound() : File(pdfContent, "application/pdf", $"Resúmen de Recuperaciones por Casos_{DateTime.Now.ToString("yyyyMMdd")}.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("generatePDFcasesamountrecovery/{id:int}")]
        public async Task<IActionResult> GeneratePDFCasesAmountRecoveryAsync(int id)
        {
            try
            {
                var pdfContent = await _case.GeneratePDFCasesAmountRecoveryAsync(id);
                return pdfContent == null ? NotFound() : File(pdfContent, "application/pdf", $"Recuperaciones del Caso {id}_{DateTime.Now.ToString("yyyyMMdd")}.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
