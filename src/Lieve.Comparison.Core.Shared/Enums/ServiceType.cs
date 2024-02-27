namespace Lieve.Comparison.Domain.Shared.Enums;

[Flags]
public enum ServiceType
{
    None = 0,
    DomesticFlight = 1,
    InternationalFlight = 2,
    Flight = DomesticFlight | InternationalFlight
}
