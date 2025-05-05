using CaseControl.Api.Interfaces;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace CaseControl.Api.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class BinnacleController : ControllerBase
    {
        private readonly IBinnacle _binnacle;

        public BinnacleController(IBinnacle binnacle)
        {
            _binnacle = binnacle;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBinnacleAsync([FromQuery] PaginationDTO pagination)
        {
            try
            {
                return Ok(await _binnacle.GetAllBinnacleAsync(pagination));
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
                return Ok(await _binnacle.GetTotalPagesAsync(pagination));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<Binnacle>> GetBinnacleAsync(int id)
        {
            try
            {
                var status = await _binnacle.GetBinnacleByIDAsync(id);
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
        public async Task<IActionResult> PutBinnacleAsync(Binnacle model)
        {
            try
            {
                return Ok(await _binnacle.EditBinnacleAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Binnacle>> PostBinnacleAsync(Binnacle model)
        {
            try
            {
                return Ok(await _binnacle.AddBinnacleAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBinnacleAsync(int id)
        {
            var exists = _binnacle.BinnacleExists(id);
            if (!exists)
            {
                return NotFound();
            }

            await _binnacle.DeleteBinnacleAsync(id);

            return NoContent();
        }

    }
}
