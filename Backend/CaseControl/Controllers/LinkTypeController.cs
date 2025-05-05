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
    public class LinkTypeController : ControllerBase
    {
        private readonly ILinkType _linkType;

        public LinkTypeController(ILinkType type)
        {
            _linkType = type;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLinkTypeAsync([FromQuery] PaginationDTO pagination)
        {
            try
            {
                return Ok(await _linkType.GetAllLinkTypeAsync(pagination));
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
                return Ok(await _linkType.GetTotalPagesAsync(pagination));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<LinkType>> GetLinkTypeAsync(int id)
        {
            try
            {
                var status = await _linkType.GetLinkTypeByIDAsync(id);
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
        public async Task<IActionResult> PutLinkTypeAsync(LinkType model)
        {
            try
            {
                return Ok(await _linkType.EditLinkTypeAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un Tipo de Vinculado con esta descripción.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<LinkType>> PostLinkTypeAsync(LinkType model)
        {
            try
            {
                return Ok(await _linkType.AddLinkTypeAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un Tipo de Vinculado con esta descripción.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLinkTypeAsync(int id)
        {
            var exists = _linkType.LinkTypeExists(id);
            if (!exists)
            {
                return NotFound();
            }

            await _linkType.DeleteLinkTypeAsync(id);

            return NoContent();
        }
    }
}
