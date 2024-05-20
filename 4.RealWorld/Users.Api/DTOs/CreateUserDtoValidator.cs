using FluentValidation;

namespace Users.Api.DTOs;

public sealed class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name cannot be null or empty");

        RuleFor(x => x.FullName).MinimumLength(3).WithMessage("Full name must be greater than 3 letter");
    }
}
