using FluentValidation;

namespace Identity.Application.Users.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage("Current password is required.")
                .MinimumLength(6).WithMessage("Current password must be at least 6 characters long.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(6).WithMessage("New password must be at least 6 characters long.")
                .NotEqual(x => x.OldPassword)
                .WithMessage("New password cannot be the same as the current password.");
        }
    }
}
