namespace FormulaOne.Application.Services.Abstractions.WriteInterfaces
{
    public interface IManageRoleService
    {
        Task<bool> SetRole(string Email,string role);
        Task<bool> RemoveFromRole(string Email,string role);
    }
}
