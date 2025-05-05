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
    public class LinkedsController : ControllerBase
    {
        private readonly ILinked _linked;

        public LinkedsController(ILinked linked)
        {
            _linked = linked;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLinkedAsync([FromQuery] PaginationDTO pagination)
        {
            try
            {
                return Ok(await _linked.GetAllLinkedAsync(pagination));
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
                return Ok(await _linked.GetTotalPagesAsync(pagination));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<Linked>> GetLinkedAsync(int id)
        {
            try
            {
                var linked = await _linked.GetLinkedByIDAsync(id);
                if (linked == null)
                {
                    return NotFound();
                }

                return Ok(linked);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> PutLinkedAsync(Linked model)
        {
            try
            {
                return Ok(await _linked.EditLinkedAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un Vinculado de Caso con esta identificación.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<Linked>> PostLinkedAsync(Linked model)
        {
            try
            {
                return Ok(await _linked.AddLinkedAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un Vinculado de Caso con esta identificación.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLinked(int id)
        {
            var exists = _linked.LinkedExists(id);
            if (!exists)
            {
                return NotFound();
            }

            await _linked.DeleteLinkedAsync(id);

            return NoContent();
        }


        [HttpGet("linktypes")]
        public async Task<IActionResult> GetAllLinkTypeAsync()
        {
            try
            {
                return Ok(await _linked.GetAllLinkTypesAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
