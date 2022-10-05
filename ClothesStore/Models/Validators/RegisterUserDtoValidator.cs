using ClothesStore.Entities;
using FluentValidation;

namespace ClothesStore.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(ClothesStoreDbContext dbContext)
        {
            RuleFor(f => f.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.");

            RuleFor(l => l.LastName)
                .NotEmpty()
                .WithMessage("Last name is required.");

            RuleFor(e => e.Email)
                .NotEmpty()
                .WithMessage("Address email is required.");

            RuleFor(e => e.Email)
                .Custom((value, context) =>
                {
                    var emailIsUsed = dbContext.Users.Any((u => u.Email == value));
                    if (emailIsUsed)
                        context.AddFailure("Email", "That email is taken.");
                });

            RuleFor(p => p.ConfirmedPassword)
                .NotEmpty()
                .Equal(p => p.Password)
                .WithMessage("Wrong email or password.");

            RuleFor(p => p.Password)
                .NotEmpty()
                .WithMessage("Wrong email or password.");

            RuleFor(p => p.PostalCode)
                .NotEmpty()
                .WithMessage("Postal code is required.");

            RuleFor(p => p.City)
                .NotEmpty()
                .WithMessage("City code is required.");

            RuleFor(p => p.Street)
                .NotEmpty()
                .WithMessage("City code is required.");

            RuleFor(p => p.StreetNumber)
                .NotEmpty()
                .WithMessage("Street number code is required.");
        }
    }
}
