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
    [Authorize (Roles = "Admin,Moderator")]
    public class SeasonWriteController(IWriteSeasonService service) : ControllerBase
    {
        private readonly IWriteSeasonService _service = service;
        [Authorize(Policy = nameof(Permissions.Create_Season))]
        [HttpPost("AddSeason")]
        public async Task<IActionResult> AddSeason([FromQuery] SeasonRequestDto dto)
        {
            await _service.AddSeason(dto);
            return Ok($"Добавлен сезон {dto.year}");
        }
        [Authorize(Policy = nameof(Permissions.Delete_Season))]
        [HttpDelete("RemoveSeason")]
        public async Task<IActionResult> RemoveSeason([FromQuery] Guid Id)
        {
            await _service.DeleteSeason(Id);
            return Ok($"Удален сезон :( , с id {Id}");
        }
    }
}
