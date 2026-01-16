using FluentValidation;
using FormulaOne.Application.Dto.Request;

namespace FormulaOne.Application.Validators
{
    public class ConstrCShRequestValidator : AbstractValidator<ConstrCShRequestDto>
    {
        public ConstrCShRequestValidator()
        {
            RuleFor(cs => cs.TeamName).MinimumLength(2);
            RuleFor(cs => cs.Points).GreaterThanOrEqualTo(0);
        }
    }
}
