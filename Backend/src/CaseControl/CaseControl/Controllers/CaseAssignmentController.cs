using CaseControl.Api.Interfaces;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CaseControl.Api.Controllers
{
    /// <summary>
    /// Controlador para gestionar las asignaciones de casos a usuarios.
    /// </summary>
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CaseAssignmentController : ControllerBase
    {
        private readonly ICaseAssignment _caseAssignmentService;

        public CaseAssignmentController(ICaseAssignment caseAssignmentService)
        {
            _caseAssignmentService = caseAssignmentService;
        }

        [ProducesResponseType(typeof(List<CaseAssignment>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
       
        [HttpGet]
        public async Task<IActionResult> GetAllCaseAssignmentAsync([FromQuery] PaginationDTO pagination)
        {
            try
            {
                var assignments = await _caseAssignmentService.GetAllCaseAssignmentAsync(pagination);
                return Ok(assignments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("totalPages")]
        [ProducesResponseType(typeof(double), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            try
            {
                var totalPages = await _caseAssignmentService.GetTotalPagesAsync(pagination);
                return Ok(totalPages);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

     
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CaseAssignment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CaseAssignment>> GetCaseAssignmentAsync(int id)
        {
            try
            {
                var caseAssignment = await _caseAssignmentService.GetCaseAssignmentByIDAsync(id);
                if (caseAssignment == null)
                {
                    return NotFound(new { message = $"No se encontró la asignación de caso con ID: {id}" });
                }

                return Ok(caseAssignment);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        
     
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(CaseAssignment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutCaseAssignmentAsync(int id, CaseAssignment model)
        {
            try
            {
                if (id != model.ID)
                {
                    return BadRequest(new { message = "El ID de la ruta no coincide con el ID del modelo." });
                }

                if (!_caseAssignmentService.CaseAssignmentExists(id))
                {
                    return NotFound(new { message = $"No se encontró la asignación de caso con ID: {id}" });
                }

                var updatedAssignment = await _caseAssignmentService.EditCaseAssignmentAsync(model);
                return Ok(updatedAssignment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

     
        [HttpPost]
        [ProducesResponseType(typeof(CaseAssignment), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CaseAssignment>> PostCaseAssignmentAsync(CaseAssignment model)
        {
            try
            {
                var newAssignment = await _caseAssignmentService.AddCaseAssignmentAsync(model);
                
                return CreatedAtAction(
                    nameof(GetCaseAssignmentAsync), 
                    new { id = newAssignment.ID }, 
                    newAssignment);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteCaseAssignmentAsync(int id)
        {
            try
            {
                var exists = _caseAssignmentService.CaseAssignmentExists(id);
                if (!exists)
                {
                    return NotFound(new { message = $"No se encontró la asignación de caso con ID: {id}" });
                }

                var result = await _caseAssignmentService.DeleteCaseAssignmentAsync(id);
                if (!result)
                {
                    return BadRequest(new { message = "No se pudo eliminar la asignación de caso." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
