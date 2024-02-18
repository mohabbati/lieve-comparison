using Lieve.Comparison.WebUi.Client.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddLieveHttpClients(new Uri(builder.Configuration.GetSection("HostedUrl").Value!));

await builder.Build().RunAsync();
