using FormulaOne.Application.Dto.Request;
using FormulaOne.Application.Exceptions;
using FormulaOne.Application.Services.Abstractions.WriteInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Controllers.WriteControllers
{
    [Route("auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController(IAuthService service) : ControllerBase
    {
        private readonly IAuthService _authService = service;
        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrRequestDto registrRequest)
        {
            try
            {
                await _authService.RegistrationAsync(registrRequest);
                return Ok("Success");
            }
            catch (RegistrationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginRequest)
        {
            try
            {
                await _authService.LoginAsync(loginRequest);
                return Ok("Success");
            }
            catch(UnauthorizedAccessException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
