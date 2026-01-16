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
    public class ConstructorCShWriteController(IWriteConstrCShService service) : ControllerBase
    {
        private readonly IWriteConstrCShService _service = service;

        [Authorize(Policy = nameof(Permissions.Create_TeamCSh))]
        [HttpPost("AddRecord")]
        public async Task<IActionResult> AddRecord([FromQuery] ConstrCShRequestDto dto)
        {
            await _service.AddConstrCShRecord(dto);
            return Ok($"Добавлена запись {dto}");
        }
    }
}
