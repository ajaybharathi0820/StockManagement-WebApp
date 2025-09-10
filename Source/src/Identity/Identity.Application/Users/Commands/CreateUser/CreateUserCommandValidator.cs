using FluentValidation;
using System;
using Identity.Domain.Repositories;

namespace Identity.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            
            RuleFor(x => x.UserName)
                .NotEmpty().MaximumLength(30)
                .MustAsync(async (username, cancellation) => !await _userRepository.IsUsernameExistsAsync(username, null, cancellation))
                .WithMessage("Username already exists. Please choose a different username.");
            
            RuleFor(x => x.Email)
                .NotEmpty().EmailAddress()
                .MustAsync(async (email, cancellation) => !await _userRepository.IsEmailExistsAsync(email, null, cancellation))
                .WithMessage("Email already exists. Please choose a different email address.");
            
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
