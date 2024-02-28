namespace Lieve.Comparison.WebUi.Client.Services.Interfaces;

public interface IAirportClient
{
    Task<IList<AirportDto>> GetAsync(LocalityType localityType, string clause, CancellationToken cancellationToken);
}
