using MediatR;
namespace FormulaOne.Application.Dto.Request
{
    public record LoginDto(string Login,string Email,string Password);

}
