using FluentValidation;
using FormulaOne.Application.Dto.Request;

namespace FormulaOne.Application.Validators
{
    public class RegistrationValidator : AbstractValidator<RegistrRequestDto>
    {
        public RegistrationValidator()
        {
            RuleFor(r => r.Login)
                .NotEmpty().MinimumLength(3).WithMessage("Минимальная длина логина 3 символа!");
            RuleFor(r => r.Password)
                .NotEmpty().MinimumLength(3).WithMessage("Минимальная длина пароля 3 символа!");
        }
    }
}
