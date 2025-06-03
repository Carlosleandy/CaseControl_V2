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
    public class FaultController : ControllerBase
    {
        private readonly IFault _fault;

        public FaultController(IFault type)
        {
            _fault = type;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFaultAsync([FromQuery] PaginationDTO pagination)
        {
            try
            {
                return Ok(await _fault.GetAllFaultsAsync(pagination));
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
                return Ok(await _fault.GetTotalPagesAsync(pagination));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<Fault>> GetFaultAsync(int id)
        {
            try
            {
                var status = await _fault.GetFaultByIDAsync(id);
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
        public async Task<IActionResult> PutFaultAsync(Fault model)
        {
            try
            {
                return Ok(await _fault.EditFaultAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una Falta con esta descripción.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<Fault>> PostFaultAsync(Fault model)
        {
            try
            {
                return Ok(await _fault.AddFaultAsync(model));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una Falta con esta descripción.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFaultAsync(int id)
        {
            var exists = _fault.FaultExists(id);
            if (!exists)
            {
                return NotFound();
            }

            await _fault.DeleteFaultAsync(id);

            return NoContent();
        }



        [HttpGet("getallfaulttypes")]
        public async Task<IActionResult> GetAllFaultTypesAsync()
        {
            try
            {
                return Ok(await _fault.GetAllFaultTypesAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
