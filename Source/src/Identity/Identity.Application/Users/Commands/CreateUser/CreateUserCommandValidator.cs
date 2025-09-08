using FluentValidation;
using System;

namespace Identity.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.DateOfBirth)
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Date of birth cannot be in the future.")
                .Must(dob => {
                    var today = DateTime.Today;
                    var age = today.Year - dob.Year;
                    if (dob.Date > today.AddYears(-age)) age--;
                    return age >= 18 && age <= 120;
                }).WithMessage("User must be between 18 and 120 years old.");
        }
    }
}
