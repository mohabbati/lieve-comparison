using System.Globalization;

namespace Lieve.Comparison.WebUi.Client.Components.FlightCriteria;

public partial class FlightCriteriaComponent
{
    private MudForm _form = default!;
    private MudDateRangePicker _dateRangePicker = default!;

    private readonly SearchModelFluentValidator searchValidator = new();
    private readonly FlightCriteriaEntered model = new();
    private readonly CultureInfo cultureInfo = new("fa-IR");

    [Parameter]
    public EventCallback<FlightCriteriaEventArgs> OnSearchSubmitted{ get; set; }

    [Inject]
    public required IAirportClient AirportClient { get; set; }

    private async Task SubmitAsync()
    {
        await _form.Validate();

        if (_form.IsValid)
        {
            var args = new FlightCriteriaEventArgs { FlightSearchModel = model };
            await OnSearchSubmitted.InvokeAsync(args);
        }
    }

    private async Task<IEnumerable<KeyValuePair<string, string>>> AirportLookup(string input, CancellationToken cancellationToken)
    {
        var airports = await AirportClient.GetAsync(LocalityType.International, input ?? string.Empty);

        var result = airports.Select(x => new KeyValuePair<string, string>(x.IataCode, $"{x.IataCode}-{x.CountryName}-{x.CityName}-{x.Name}")).ToList();

        return result;
    }

    public class SearchModelFluentValidator : AbstractValidator<FlightCriteriaEntered>
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
            var result = await ValidateAsync(ValidationContext<FlightCriteriaEntered>.CreateWithOptions((FlightCriteriaEntered)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return [];
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
