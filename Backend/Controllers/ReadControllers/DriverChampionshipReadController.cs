using FormulaOne.Application.Services.Abstractions.ReadInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Controllers.ReadControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class DriverChampionshipReadController(IReadDriverCShService service) 
        : ControllerBase
    {
        private readonly IReadDriverCShService _service = service;
        [HttpGet("GetDriversTable")]
        public async Task<IActionResult> GetDriversTableByYear([FromQuery] short Year)
        {
            var result = await _service.GetDriversTable(Year);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }
    }
}
