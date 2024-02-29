namespace Lieve.Comparison.Domain.Shared.Enums;

[Flags]
public enum LocalityType
{
    None = 0,
    Domestic = 1,
    International = 2,
    Anywhere = Domestic | International
}
