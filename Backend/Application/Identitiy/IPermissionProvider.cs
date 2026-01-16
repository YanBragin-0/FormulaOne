using FormulaOne.Enums;

namespace FormulaOne.Application.Identity
{
    public interface IPermissionProvider
    {
        IReadOnlyCollection<Enums.Permissions> GetPermissionsForRole(string role);
    }
}
