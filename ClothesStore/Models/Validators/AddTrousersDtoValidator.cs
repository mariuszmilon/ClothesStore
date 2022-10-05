using FluentValidation;

namespace ClothesStore.Models.Validators
{
    public class AddTrousersDtoValidator : AbstractValidator<AddTrousersDto>
    {
        private string[] allowedTrousersType = new string[] { "Straight", "Slim", "Skinny", "Loose", "Wide", "Flared", "Tapered", "Regular" };
        public AddTrousersDtoValidator()
        {
            RuleFor(p => p.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .ScalePrecision(2, 5)
                .WithMessage("The pricee must be greater or equals 1.");

            RuleFor(u => u.Unused)
                .NotEmpty()
                .WithMessage("You must give a information about trousers condition.");

            RuleFor(b => b.Brand)
                .NotEmpty()
                .WithMessage("Brand is required.");


            RuleFor(u => u.Title)
                .NotEmpty()
                .WithMessage("Title is required.");

            RuleFor(u => u.Title)
                .MaximumLength(100)
                .WithMessage("Title is too long.");


            RuleFor(r => r.Type)
                .Custom((value, context) =>
                {
                    if (!allowedTrousersType.Contains(value))
                        context.AddFailure("Type", "Incorrect trousers type.");
                });

            RuleFor(s => s.Sex)
                .IsEnumName(typeof(AllowedSexType))
                .WithMessage("Incorrect sex type.");

            RuleFor(k => k.Size)
                .IsEnumName(typeof(AllowedSize))
                .WithMessage("Incorrect size.");
        }
    }
}
