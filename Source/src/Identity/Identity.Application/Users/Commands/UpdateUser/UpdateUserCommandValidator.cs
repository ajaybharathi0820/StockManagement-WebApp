using FluentValidation;
using System;

namespace Identity.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("User Id is required.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.DateOfBirth)
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Date of birth cannot be in the future.")
                .Must(dob => {
                    var today = DateTime.Today;
                    var age = today.Year - dob.Year;
                    if (dob.Date > today.AddYears(-age)) age--;
                    return age >= 18 && age <= 120;
                }).WithMessage("User must be between 18 and 120 years old.");

            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("Role is required.");
        }
    }
}
