using System.Globalization;

namespace Lieve.Comparison.WebUi.Client.Components.LieveDateRangePicker;

public partial class LieveDateRangePickerComponent
{
    private DateRange _dateRange = default!;

    [Parameter]
    public DateRange DateRange
    {
        get => _dateRange;
        set
        {
            if (value == _dateRange) return;
            _dateRange = value;
            _ = DateRangeChanged.InvokeAsync(value);
        }
    }

    [Parameter]
    public EventCallback<DateRange> DateRangeChanged { get; set; }

    private MudDateRangePicker _dateRangePicker = default!;
    private CultureInfo _cultureInfo = new("fa-IR");
    private DateTime? lastPicker;

    private void ChangeCulture()
    {
        lastPicker ??= _dateRangePicker.PickerMonth ?? DateTime.Now;

        if (_cultureInfo.Name == "fa-IR")
        {
            _cultureInfo = new CultureInfo("en-US");
        }
        else
        {
            _cultureInfo = new CultureInfo("fa-IR");
        }
        lastPicker = null;
    }
}
