using System.Reflection;
using Lieve.Comparison.Application.Common.Behaviours;
using Microsoft.Extensions.DependencyInjection;

namespace Lieve.Comparison.Application;

public static class Configurations
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(config => {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        });
        
        return services;
    }
}