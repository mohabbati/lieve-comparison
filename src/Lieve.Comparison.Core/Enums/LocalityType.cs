namespace Lieve.Comparison.Core.Enums;

[Flags]
public enum LocalityType
{
    Domestic = 1,
    International = 2,
    Anywhere = Domestic | International
}
