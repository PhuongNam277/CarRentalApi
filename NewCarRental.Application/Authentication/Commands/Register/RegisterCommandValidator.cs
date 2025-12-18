using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace NewCarRental.Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator() {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Fullname cannot be empty")
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress().WithMessage("Email must be correct form");

            RuleFor(x => x.PasswordHash)
                .NotEmpty()
                .MinimumLength(8).WithMessage("Password must be have at least 8 characters");
        }
    }
}
