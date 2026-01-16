using FluentValidation;
using FormulaOne.Application.Dto.Request;

namespace FormulaOne.Application.Validators
{
    public class RaceRequestValidator : AbstractValidator<RaceRequestDto>
    {
        public RaceRequestValidator()
        {
            RuleFor(r => r.Title).NotEmpty();
            RuleFor(r => r.CircuitId).NotNull();
        }
    }
}
