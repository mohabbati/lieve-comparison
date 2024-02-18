using System.Globalization;

namespace Lieve.Comparison.WebUi.Client.Components;

public partial class AirportSearch
{
    private MudForm form = default!;
    private SearchModelFluentValidator searchValidator = new();
    private SearchModel model = new();
    private MudDateRangePicker _picker = default!;
    private CultureInfo _cultureInfo = new CultureInfo("fa-IR");

    [Inject]
    public required IAirportClient AirportClient { get; set; }

    public class SearchModel
    {
        public string Origin { get; set; } = default!;
        public string Destination { get; set; } = default!;
        public DateRange? DateRange { get; set; }
        public int Adl { get; set; } = 1;
        public int Chd { get; set; }
        public int Inf { get; set; }
    }

    private async Task<IEnumerable<string>> Search(string value, CancellationToken token)
    {
        var airports = await AirportClient.GetAsync(LocalityType.International, value ?? string.Empty);

        var result = airports.Select(item => $"{item.IataCode}-{item.CountryName}-{item.CityName}-{item.Name}").ToList();

        return result;
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

    private async Task Submit()
    {
        await form.Validate();

        if (form.IsValid)
        {
            //
        }
    }
}
