using FormulaOne.Application.Dto.Request;
using FormulaOne.Application.Exceptions;
using FormulaOne.Application.Services.Abstractions.WriteInterfaces;
using FormulaOne.Entities;
using FormulaOne.Shared;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FormulaOne.Application.Services.WriteServices
{
    public class AuthService(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager) : IAuthService
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly SignInManager<AppUser> _signInManager = signInManager;

        public async Task LoginAsync(LoginDto login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Login, login.Password, true, false);
            if (!result.Succeeded) 
            {
                throw new UnauthorizedAccessException();
            }
        }

        public async Task RegistrationAsync(RegistrRequestDto registr)
        {
            var user = new AppUser
            {
                UserName = registr.Login,
                Email = registr.Email,
            };
            var result = await _userManager.CreateAsync(user,registr.Password);
            if (!result.Succeeded) 
            {
                var errors = result.Errors.Select(e => e.Description ?? "Unknown Registration Exception").ToList();
                throw new RegistrationException(errors);
            }
            await _userManager.AddToRoleAsync(user, "SimpleUser");
        }
    }
}
