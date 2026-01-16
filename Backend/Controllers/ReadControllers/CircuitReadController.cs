using FormulaOne.Application.Services.Abstractions.ReadInterfaces;
using FormulaOne.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Controllers.ReadControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = nameof(Permissions.Read_Circuit))]
    public class CircuitReadController(IReadCircuitService service) : ControllerBase
    {
        private readonly IReadCircuitService _service = service;

        [HttpGet("GetAllCircuits")]
        public async Task<IActionResult> GetAllCircuits()
        {
            var result = await _service.GetAllCircuits();
            return result.IsSuccess ?  Ok(result.Value) : NotFound(result.ErrorMessage);
        }
        [HttpGet("getById")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById([FromBody] Guid Id)
        {
            var result = await _service.GetById(Id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }
    }
}
