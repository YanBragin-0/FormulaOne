using FormulaOne.Application.Services.Abstractions.WriteInterfaces;
using FormulaOne.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Controllers.WriteControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Policy = nameof(Permissions.User_Manage))]
    public class RoleManagerController(IManageRoleService service) : ControllerBase
    {
        private readonly IManageRoleService _service = service;

        [HttpPost("SetRole")]
        public async Task<IActionResult> SetRoleToUser(string Email,string Role)
        {
            var result = await _service.SetRole(Email,Role);
            return result != false ?  Ok("Success") : BadRequest("Неверный Email/Role");
        }
        [HttpPost("RemoveFromRole")]
        public async Task<IActionResult> RemoveFromRole(string Email,string Role)
        {
            var result = await _service.RemoveFromRole(Email,Role);
            return result != false ?  Ok("Success") : BadRequest("Неверный Email/Role");        }
    }
}
