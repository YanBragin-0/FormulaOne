using FluentValidation;
using FormulaOne.Application.Dto.Request;

namespace FormulaOne.Application.Validators
{
    public class DriverRequestValidator : AbstractValidator<DriverRequestDto> 
    {
        public DriverRequestValidator()
        {
            RuleFor(d => d.Name).MinimumLength(3);
            RuleFor(d => d.Age).GreaterThanOrEqualTo((short)16);
            RuleFor(d => d.Country).NotEmpty();
        }
    }
}
