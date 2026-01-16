using FormulaOne.Application.Services.Abstractions.ReadInterfaces;
using FormulaOne.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Controllers.ReadControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = nameof(Permissions.Read_Drivers))]
    public class DriversReadController(IReadDriverService service) : ControllerBase
    {
        private readonly IReadDriverService _service = service;

        [HttpGet("GetAllDrivers")]
        public async Task<IActionResult> GetAllDrivers()
        {
            var result = await _service.GetAllDrivers();
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }
        [HttpGet("GetTeamDrivers")]
        public async Task<IActionResult> GetTeamDrivers([FromQuery] string Team)
        {
            var result = await _service.GetTeamDrivers(Team);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }
    }
}
