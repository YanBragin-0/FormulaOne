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
    public class TeamWriteController(IWriteTeamService service) : ControllerBase
    {
        private readonly IWriteTeamService _service = service;
        [Authorize(Policy = nameof(Permissions.Create_Teams))]
        [HttpPost("AddTeam")]
        public async Task<IActionResult> AddTeam([FromQuery] TeamRequestDto dto) 
        { 
            await _service.AddTeam(dto);
            return Ok($"Добавлена команда {dto.TeamName}");
        }
        [HttpPatch("AddBiography")]
        public async Task<IActionResult> AddBiography([FromQuery] Guid TeamId, 
                                                        [FromQuery] TeamRequestDto dto) 
        {
            await _service.AddBiography(TeamId,dto);
            return Ok($"Добавлена биография : {dto.Biography}");
        }
        [Authorize(Policy = nameof(Permissions.Delete_Teams))]
        [HttpDelete("RemoveTeam")]
        public async Task<IActionResult> RemoveTeam([FromQuery] Guid Id)
        {
            await _service.DeleteTeam(Id);
            return Ok("Команда удалена");
        }
    }
}
