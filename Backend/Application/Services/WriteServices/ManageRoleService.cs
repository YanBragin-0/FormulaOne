using FormulaOne.Application.Services.Abstractions.WriteInterfaces;
using FormulaOne.Entities;
using Microsoft.AspNetCore.Identity;
using FormulaOne.Enums;
using FormulaOne.Application.SharedAbstractions;

namespace FormulaOne.Application.Services.WriteServices
{
    public class ManageRoleService(UserManager<AppUser> user,
                                ICurrentUser currentUser) : IManageRoleService
    {
        private readonly UserManager<AppUser> _userManager = user;
        private readonly ICurrentUser _currentUser = currentUser;

        public async Task<bool> RemoveFromRole(string Email, string role)
        {
            var result = await CheckAndValidate(Email,role);
            if(result.Item2 != false)
            {
                var IdentityResult = await _userManager.RemoveFromRoleAsync(result.Item1!, role);
                if (IdentityResult != null && IdentityResult.Succeeded)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public async Task<bool> SetRole(string Email, string role)
        {
            var result = await CheckAndValidate(Email,role);
            if (result.Item2 != false)
            {
                await _userManager.AddToRoleAsync(result.Item1!, role);
                return true;
            }
            return false;

        }
        private async Task<Tuple<AppUser?,bool>> CheckAndValidate(string Email, string role)
        {
            if (!Enum.TryParse<Roles>(role, out var result) || !Enum.IsDefined(typeof(Roles), result))
            {
                return Tuple.Create<AppUser?,bool>(default,false);
            }
            var currentRoles = _currentUser.Roles.Select(r => (int)Enum.Parse<Roles>(r)).ToList();
            if (currentRoles.Max() < (int)Enum.Parse<Roles>(role))
            {
                return Tuple.Create<AppUser?, bool>(default, false);
            }
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return Tuple.Create<AppUser?, bool>(default, false);
            }

            return Tuple.Create<AppUser?, bool>(user, true);
        }
    }
}
