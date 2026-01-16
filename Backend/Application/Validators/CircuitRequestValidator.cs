using FluentValidation;
using FormulaOne.Application.Dto.Request;

namespace FormulaOne.Application.Validators
{
    public class CircuitRequestValidator : AbstractValidator<CircuitRequestDto>
    {
        public CircuitRequestValidator()
        {
            RuleFor(c => c.Title).NotEmpty().MinimumLength(3);
            RuleFor(c => c.Country).NotEmpty().MinimumLength(2);
        }
    }
}
