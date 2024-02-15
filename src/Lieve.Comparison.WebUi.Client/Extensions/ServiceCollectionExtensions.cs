using Lieve.Comparison.WebUi.Client.Services.Implementations;
using Lieve.Comparison.WebUi.Client.Services.Interfaces;

namespace Lieve.Comparison.WebUi.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLieveClients(this IServiceCollection services, Uri baseAddress)
    {
        services.AddHttpClient<IAirportClient, AirportClient>(httpClient =>
        {
            httpClient.BaseAddress = baseAddress;
        });

        return services;
    }
}
