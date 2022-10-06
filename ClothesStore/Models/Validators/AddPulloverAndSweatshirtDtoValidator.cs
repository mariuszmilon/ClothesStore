using FluentValidation;

namespace ClothesStore.Models.Validators
{
    public class AddPulloverAndSweatshirtDtoValidator : AbstractValidator<AddPulloverAndSweatshirtDto>
    {
        public AddPulloverAndSweatshirtDtoValidator()
        {
            RuleFor(p => p.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .ScalePrecision(2, 5)
                .WithMessage("The pricee must be greater or equals 1.");

            RuleFor(u => u.Unused)
                .NotEmpty()
                .WithMessage("You must give a information about shoes condition.");

            RuleFor(b => b.Brand)
                .NotEmpty()
                .WithMessage("Brand is required.");


            RuleFor(u => u.Title)
                .NotEmpty()
                .WithMessage("Title is required.");

            RuleFor(u => u.Title)
                .MaximumLength(100)
                .WithMessage("Title is too long.");

            RuleFor(s => s.Sex)
                .IsEnumName(typeof(AllowedSexType))
                .WithMessage("Incorrect sex type.");

            RuleFor(u => u.Hood)
                .NotEmpty()
                .WithMessage("Hood is required.");

            RuleFor(u => u.Decolletage)
                .NotEmpty()
                .WithMessage("Decolletage is required.");
        }
    }
}
