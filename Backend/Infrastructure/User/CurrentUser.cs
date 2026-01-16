using FormulaOne.Application.SharedAbstractions;
using FormulaOne.Enums;
using System.Security.Claims;

namespace FormulaOne.Infrastructure.User
{
    public class CurrentUser(IHttpContextAccessor contextAccessor) : ICurrentUser
    {
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

        public Guid Id=> Guid.TryParse(_contextAccessor.HttpContext?.User
                            ?.FindFirstValue(ClaimTypes.NameIdentifier),out var id) 
                                                                        ? id : Guid.Empty;
        public bool IsAuthenticated => _contextAccessor!.HttpContext!.User!.Identity!.IsAuthenticated;
        public IReadOnlyCollection<string> Roles => _contextAccessor.HttpContext?.User
                    ?.FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray() 
                                                        ?? Array.Empty<string>();

        
        public bool IsInRole(Roles role)
        {
            return Roles.Any(r => Enum.TryParse<Roles>(r, out var success) && success == role);
        } 

    }
}
