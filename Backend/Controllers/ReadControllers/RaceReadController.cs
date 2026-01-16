using FormulaOne.Application.Services.Abstractions.ReadInterfaces;
using FormulaOne.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Controllers.ReadControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = nameof(Permissions.Read_Races))]
    public class RaceReadController(IReadRaceService service) : ControllerBase
    {
        private readonly IReadRaceService _service = service;
        [HttpGet("GetAllRaces")]
        public async Task<IActionResult> GetAllRaces()
        {
            var result = await _service.GetAllRaces();
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }
        [HttpGet("GetCountryRace")]
        public async Task<IActionResult> GetByCountry([FromQuery] string Country)
        {
            var result = await _service.GetRaceByCountry(Country);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }
    }
}
