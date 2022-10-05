using FluentValidation;

namespace ClothesStore.Models.Validators
{
    public class QueryValidator : AbstractValidator<AnnouncementQuery>
    {
        private int[] allowedPages = new int[] { 5, 10, 15 };
        private string[] allowedColumn = new string[] { "PublicationDate", "Price", "Brand" };
        public QueryValidator()
        {

            RuleFor(e => e.PageNumber).GreaterThanOrEqualTo(1);

            RuleFor(r => r.PageSize)
                .Custom((value, context) =>
                {
                    if (!allowedPages.Contains(value))
                    {
                        context.AddFailure("PageSize", "Page size must be in range [5 10 15].");
                    }
                });

            RuleFor(r => r.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedColumn.Contains(value))
                .WithMessage("Sort by must sorted by: PublicationDate, Price or Brand.");

         }
    }
}
