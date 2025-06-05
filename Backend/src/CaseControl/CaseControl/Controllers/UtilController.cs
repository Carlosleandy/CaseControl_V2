using CaseControl.Api.Helpers;
using CaseControl.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseControl.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilController : ControllerBase
    {
        private readonly IDbUtil _util;

        public UtilController(IDbUtil util)
        {
            _util = util;
        }


        [HttpGet("GetAllOffices")]
        public async Task<IActionResult> GetAllOffices()
        {
            try
            {
                return Ok(await _util.GetAllOfficesAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetAllCostCenters")]
        public async Task<IActionResult> GetAllCostCenters()
        {
            try
            {
                return Ok(await _util.GetAllCostCentersAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetEmployees/{ident}")]
        public async Task<IActionResult> GetEmployees(string ident)
        {
            try
            {
                return Ok(await _util.GetEmployeeByCodeIdentAsync(ident));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
