using FormulaOne.Application.Dto.Request;
using FormulaOne.Application.Services.Abstractions.ReadInterfaces;
using FormulaOne.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Controllers.ReadControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = nameof(Permissions.Read_Cars))]
    public class CarsReadController(IReadCarService service) : ControllerBase
    {
        private readonly IReadCarService _service = service;
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCars()
        {
            var result = await _service.GetAllCars();
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }
        [HttpGet("GetTeamCar")]
        public async Task<IActionResult> GetTeamCar([FromQuery] TeamRequestDto team, [FromQuery] SeasonRequestDto season)
        {
            var result = await _service.GetTeamCar(team,season);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }

    }
}
