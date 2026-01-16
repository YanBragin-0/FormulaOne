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
    public class DriversWriteController(IWriteDriverService service) : ControllerBase
    {
        private readonly IWriteDriverService _service = service;
        [Authorize(Policy = nameof(Permissions.Create_Drivers))]
        [HttpPost("AddDriver")]
        public async Task<IActionResult> AddDriver([FromQuery] DriverRequestDto dto)
        {
            await _service.AddDriver(dto);
            return Ok($"Добавлен пилот {dto.Name}");
        }
        [Authorize(Policy = nameof(Permissions.Delete_Drivers))]
        [HttpDelete("RemoveDriver")]
        public async Task<IActionResult> RemoveDriver([FromQuery] Guid Id) 
        {
            await _service.DeleteDriver(Id);
            return Ok($"Удалён пилот :( , с id {Id}");
        }
    }
}
