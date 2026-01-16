using FluentValidation;
using FormulaOne.Application.Dto.Request;

namespace FormulaOne.Application.Validators
{
    public class DriverCShRequestValidator : AbstractValidator<DriverCShRequestDto> 
    {
        public DriverCShRequestValidator()
        {
            RuleFor(d => d.DriverName).MinimumLength(3);
            RuleFor(d => d.Points).GreaterThanOrEqualTo(0);
        }
    }
}
