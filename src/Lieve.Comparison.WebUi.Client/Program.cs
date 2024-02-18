using Lieve.Comparison.WebUi.Client.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSharedServices(builder.Configuration);

await builder.Build().RunAsync();
