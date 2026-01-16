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
    public class CarsWriteController(IWriteCarService service) : ControllerBase
    {
        private readonly IWriteCarService _service = service;
        [Authorize(Policy = nameof(Permissions.Create_Cars))]
        [HttpPost("addCar")]
        public async Task<IActionResult> AddCar([FromQuery] CarRequestDto dto)
        {
            var result = await _service.AddCar(dto);
            return result == true ? Ok("Болид добавлен") 
                : BadRequest("У команды может быть только один болид на сезон!");
        }
        [Authorize(Policy = nameof(Permissions.Delete_Cars))]
        [HttpDelete("removeCar")]
        public async Task<IActionResult> RemoveCar([FromQuery]Guid Id, [FromQuery]string TeamName,[FromQuery]string year)
        {
            await _service.DeleteCar(Id,TeamName,year);
            return Ok();
        }
    }
}
