using Lieve.Comparison.WebUi.Client.Services.Implementations;
using MudBlazor.Services;

namespace Lieve.Comparison.WebUi.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLieveHttpClients(new Uri(configuration.GetSection("HostedUrl").Value!));
        services.AddMudServices(x => x.PopoverOptions.ThrowOnDuplicateProvider = false);

        return services;
    }

    private static IServiceCollection AddLieveHttpClients(this IServiceCollection services, Uri baseAddress)
    {
        services.AddHttpClient<IAirportClient, AirportClient>(httpClient =>
        {
            httpClient.BaseAddress = baseAddress;
        });

        services.AddHttpClient<IVendorClient, VendorClient>(httpClient =>
        {
            httpClient.BaseAddress = baseAddress;
        });

        services.AddHttpClient<IVendorUrlClient, VendorUrlClient>(httpClient =>
        {
            httpClient.BaseAddress = baseAddress;
        });

        return services;
    }
}
