using Lieve.Comparison.Application;
using Lieve.Comparison.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddCors(options =>
    {
        var allowedOrigins = builder.Configuration["AllowedHosts"]!.Split(",");
        options.AddPolicy("app-policy",
            builder =>
            {
                builder.WithOrigins(allowedOrigins)
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
    })
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("app-policy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
