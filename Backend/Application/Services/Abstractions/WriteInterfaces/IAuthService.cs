using FormulaOne.Application.Dto.Request;
using FormulaOne.Shared;

namespace FormulaOne.Application.Services.Abstractions.WriteInterfaces
{
    public interface IAuthService
    {
        Task RegistrationAsync(RegistrRequestDto registr);
        Task LoginAsync(LoginDto login);
    }
}
