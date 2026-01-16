using FormulaOne.Application.Services.Abstractions.ReadInterfaces;
using FormulaOne.Entities;
using FormulaOne.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FormulaOne.Controllers.ReadControllers
{
    [Route("auth")]
    [ApiController]
    [AllowAnonymous]
    
    public class GoogleController(UserManager<AppUser> userManager,
                                    SignInManager<AppUser> signInManager,
                                    IConfiguration configuration) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        IConfiguration _configuration = configuration;
        [HttpGet("loginWith/google")]
        public IActionResult LoginGoogle()
        {
            var redirectUrl = Url.Action("GoogleCallBack", "Google");
            var props = _signInManager.ConfigureExternalAuthenticationProperties(GoogleDefaults.AuthenticationScheme, redirectUrl);
            return Challenge(props,GoogleDefaults.AuthenticationScheme);
        }
        [HttpGet("continue-with-google")]
        public async Task<IActionResult> GoogleCallBack() 
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
            if (!authenticateResult.Succeeded) 
            {
                return BadRequest("Authentication Error (Google)");
            }
            var principal = authenticateResult.Principal;
            var email = principal?.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) { return BadRequest("Email NotFound"); }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) 
            {
                user = new AppUser
                {
                    Email = email,
                    UserName = email
                };
                var Result = await _userManager.CreateAsync(user);
                if (!Result.Succeeded)
                {
                    return BadRequest(Result.Errors.Select(e => e.Description));
                }
                await _userManager.AddToRoleAsync(user,Roles.SimpleUser.ToString());
            }
            await _signInManager.SignInAsync(user, isPersistent: false);
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            var redirectUrl = _configuration["Angular:RedirectUrl"] ?? "http://localhost:4200";
            return Redirect(redirectUrl);
        }
    }
}
