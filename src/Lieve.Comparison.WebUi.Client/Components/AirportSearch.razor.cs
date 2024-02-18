using System.Globalization;

namespace Lieve.Comparison.WebUi.Client.Components;

public partial class AirportSearch
{
    private MudForm _form = default!;
    private MudDateRangePicker _dateRangePicker = default!;

    private readonly SearchModelFluentValidator searchValidator = new();
    private readonly SearchModel model = new();
    private readonly CultureInfo cultureInfo = new("fa-IR");

    [Inject]
    public required IAirportClient AirportClient { get; set; }

    private async Task Submit()
    {
        await _form.Validate();

        if (_form.IsValid)
        {
            //
        }
    }

    private async Task<IEnumerable<string>> AirportLookup(string value, CancellationToken token)
    {
        var airports = await AirportClient.GetAsync(LocalityType.International, value ?? string.Empty);

        var result = airports.Select(item => $"{item.IataCode}-{item.CountryName}-{item.CityName}-{item.Name}").ToList();

        return result;
    }

    public class SearchModel
    {
        public string Origin { get; set; } = default!;
        public string Destination { get; set; } = default!;
        public DateRange? DateRange { get; set; }
        public int Adl { get; set; } = 1;
        public int Chd { get; set; }
        public int Inf { get; set; }
    }

    public class SearchModelFluentValidator : AbstractValidator<SearchModel>
    {
        public SearchModelFluentValidator()
        {
            RuleFor(x => x.Origin)
                .NotEmpty();

            RuleFor(x => x.Destination)
                .NotEmpty();

            RuleFor(x => x.DateRange)
                .NotNull()
                .NotEmpty();
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<SearchModel>.CreateWithOptions((SearchModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return [];
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
