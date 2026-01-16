using FormulaOne.Application.Services.Abstractions.ReadInterfaces;
using FormulaOne.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Controllers.ReadControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = nameof(Permissions.Read_Teams))]
    public class TeamReadController(IReadTeamService service) : ControllerBase
    {
        private readonly IReadTeamService _service = service;

        [HttpGet("GetAllTeams")]
        public async Task<IActionResult> GetAllTeams() 
        { 
            var result = await _service.GetAllTeams();
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }
        [HttpGet("GetTeamByName")]
        public async Task<IActionResult> GetTeamByName([FromQuery] string TeamName)
        {
            var result = await _service.GetTeamByName(TeamName);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.ErrorMessage);
        }
    }
}
