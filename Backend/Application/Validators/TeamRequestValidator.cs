using FluentValidation;
using FormulaOne.Application.Dto.Request;

namespace FormulaOne.Application.Validators
{
    public class TeamRequestValidator : AbstractValidator<TeamRequestDto> 
    {
        public TeamRequestValidator()
        {
            RuleFor(t => t.TeamName).MinimumLength(3);
        }
    }
}
