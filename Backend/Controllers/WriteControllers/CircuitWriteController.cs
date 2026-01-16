using FormulaOne.Application.Dto.Request;
using FormulaOne.Application.Services.Abstractions.WriteInterfaces;
using FormulaOne.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Controllers.WriteControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Moderator")]
    public class CircuitWriteController(IWriteCircuitService service) : ControllerBase
    {
        private readonly IWriteCircuitService _circuitService = service;
        [Authorize(Policy = nameof(Permissions.Create_Circuit))]
        [HttpPost("AddCircuit")]
        public async Task<IActionResult> AddCircuit([FromQuery] CircuitRequestDto dto)
        {
            await _circuitService.AddCircuit(dto);
            return Ok($"Добавлена трасса {dto.Title}");
        }
        [Authorize(Policy = nameof(Permissions.Delete_Circuit))]
        [HttpDelete("RemoveCircuit")]
        public async Task<IActionResult> RemoveCircuit([FromQuery] Guid Id)
        {
            await _circuitService.RemoveCircuit(Id);
            return Ok();
        }

    }
}
