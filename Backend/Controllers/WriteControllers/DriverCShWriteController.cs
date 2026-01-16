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
    public class DriverCShWriteController(IWriteDriverCShService service) : ControllerBase
    {
        private readonly IWriteDriverCShService _service = service;

        [Authorize(Policy = nameof(Permissions.Create_DriversCSh))]
        [HttpPost("AddRecord")]
        public async Task<IActionResult> AddRecord([FromQuery] DriverCShRequestDto dto)
        {
            await _service.AddDriverCShRecord(dto);
            return Ok($"Добавлена запись {dto}");
        }
    }
}
