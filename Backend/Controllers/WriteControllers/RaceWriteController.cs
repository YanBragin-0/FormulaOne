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
    public class RaceWriteController(IWriteRaceService service) : ControllerBase
    {
        private readonly IWriteRaceService _service = service;
        [Authorize(Policy = nameof(Permissions.Create_Races))]
        [HttpPost("AddRace")]
        public async Task<IActionResult> AddRace([FromQuery] RaceRequestDto dto) 
        { 
            await _service.AddRace(dto);
            return Ok($"Добавлена гонка {dto.Title}");
        }
        [Authorize(Policy = nameof(Permissions.Delete_Races))]
        [HttpDelete("RemoveRace")]
        public async Task<IActionResult> RemoveRace([FromQuery] Guid Id) 
        { 
            await _service.DeleteRace(Id);
            return Ok($"Удалена трасса :( , с id {Id}");
        }
    }
}
