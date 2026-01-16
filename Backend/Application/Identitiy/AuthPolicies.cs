using FormulaOne.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace FormulaOne.Application.Identitiy
{
    public static class AuthPolicies
    {
        public static void AddPermissionPolicies(this AuthorizationOptions options)
        {
            foreach (var permissions in Enum.GetValues<Permissions>())
            {
                options.AddPolicy(permissions.ToString(), policy => policy.RequireClaim("permission", permissions.ToString()));
            }
        }
    }
}
