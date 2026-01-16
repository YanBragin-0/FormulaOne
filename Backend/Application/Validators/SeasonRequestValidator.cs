using FluentValidation;
using FormulaOne.Application.Dto.Request;

namespace FormulaOne.Application.Validators
{
    public class SeasonRequestValidator : AbstractValidator<SeasonRequestDto> 
    {
        public SeasonRequestValidator()
        {
            RuleFor(s => s.year).GreaterThanOrEqualTo((short)1950);
        }
    }
}
