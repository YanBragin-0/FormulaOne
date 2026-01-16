using FluentValidation;
using FormulaOne.Application.Dto.Request;

namespace FormulaOne.Application.Validators
{
    public class CarRequestValidator : AbstractValidator<CarRequestDto>
    {
        public CarRequestValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty().MinimumLength(2).WithMessage("Минимальная длина названия 2 символа!");
        }
    }
}
