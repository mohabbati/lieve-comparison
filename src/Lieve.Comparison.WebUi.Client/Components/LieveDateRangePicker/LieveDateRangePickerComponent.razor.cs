using System.Globalization;
using System.Reflection;

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
    private CultureInfo _cultureInfo = GetPersianCulture();
    private string _titleDateFormat = "dddd, dd MMMM,yyyy";
    private DateTime? _lastPicker;

    private void ChangeCulture()
    {
        _lastPicker ??= _dateRangePicker.PickerMonth ?? DateTime.Now;

        if (_cultureInfo.Name == "fa-IR")
        {
            _cultureInfo = new CultureInfo("en-US");
            _titleDateFormat = "ddd, dd MMM";
        }
        else
        {
            _cultureInfo = GetPersianCulture();
            _titleDateFormat = "dddd, dd MMMM,yyyy";
        }
        _lastPicker = null;
    }

    private static CultureInfo GetPersianCulture()
    {
        var culture = new CultureInfo("fa-IR");
        var formatInfo = culture.DateTimeFormat;

        formatInfo.AbbreviatedDayNames = ["ی", "د", "س", "چ", "پ", "ج", "ش"];
        formatInfo.DayNames = ["یکشنبه", "دوشنبه", "سه شنبه", "چهار شنبه", "پنجشنبه", "جمعه", "شنبه"];
        
        var monthNames = new[]
        {
            "فروردین", "اردیبهشت", "خرداد",
            "تیر", "مرداد", "شهریور",
            "مهر", "آبان", "آذر",
            "دی", "بهمن", "اسفند",
            "",
        };

        formatInfo.AbbreviatedMonthNames =
            formatInfo.MonthNames =
                formatInfo.MonthGenitiveNames = formatInfo.AbbreviatedMonthGenitiveNames = monthNames;
        formatInfo.AMDesignator = "ق.ظ";
        formatInfo.PMDesignator = "ب.ظ";
        formatInfo.ShortDatePattern = "yyyy/MM/dd";
        formatInfo.LongDatePattern = "dddd, dd MMMM,yyyy";
        formatInfo.FirstDayOfWeek = DayOfWeek.Saturday;

        var calendar = new PersianCalendar();
        var fieldInfo = culture.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance)!;
        
        fieldInfo?.SetValue(culture, calendar);
        
        var info = formatInfo.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance)!;
        
        info?.SetValue(formatInfo, calendar);
        culture.NumberFormat.NumberDecimalSeparator = "/";
        culture.NumberFormat.DigitSubstitution = DigitShapes.NativeNational;
        culture.NumberFormat.NumberNegativePattern = 0;
        
        return culture;
    }
}
