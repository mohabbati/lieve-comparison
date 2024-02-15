using Lieve.Comparison.WebUi.Client.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddLieveClients(new Uri(builder.HostEnvironment.BaseAddress));

await builder.Build().RunAsync();
