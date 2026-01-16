using FormulaOne.Application.Identity;
using FormulaOne.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FormulaOne.Application.Identitiy
{
    public class PermissionClaimsTransformation(IPermissionProvider provider,
                                                UserManager<AppUser> userManager) : IClaimsTransformation
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IPermissionProvider _permissionProvider = provider;
        #pragma warning disable CS1998 // В асинхронном методе отсутствуют операторы await, будет выполнен синхронный метод
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        #pragma warning restore CS1998 // В асинхронном методе отсутствуют операторы await, будет выполнен синхронный метод
        {
            if (!principal.Identity?.IsAuthenticated ?? true)
                return principal;
            var userId = _userManager.GetUserId(principal);
            if (userId == null)
            {
                return principal;
            }
            var roles = principal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            var permission = roles.SelectMany(r => _permissionProvider.GetPermissionsForRole(r)).Distinct().ToList(); 
            var identity = (ClaimsIdentity)principal.Identity!;
            var existingCollectionClaims = identity.Claims.ToList();
            foreach (var p in permission)
            {
                if (!existingCollectionClaims.Any(c => c.Type == "permission" && c.Value == p.ToString()))
                {
                    identity.AddClaim(new Claim("permission", p.ToString()));
                }
            }
            return principal;
        }
    }
}
