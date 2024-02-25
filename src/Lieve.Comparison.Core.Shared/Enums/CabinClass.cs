namespace Lieve.Comparison.Domain.Shared.Enums;

[Flags]
public enum CabinClass
{
    Economy = 1,
    PremiumEconomy = 2,
    Business = 4,
    PremiumBusiness = 8,
    First = 16,
    PremiumFirst = 32
}
