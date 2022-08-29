using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace UniquomeApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
#if DEBUG
        Console.WriteLine($"Executing Assembly at DI of Application: {Assembly.GetExecutingAssembly()}");
#endif
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        // services.AddScoped(typeof(IVesselImporter), typeof(VesselImporter));
        // services.AddScoped(typeof(IVesselPositionImporter), typeof(VesselPositionImporter));
        // // services.AddScoped(typeof(IStaticTimeIntervalCache), typeof(StaticTimeIntervalCache));
        // services.AddSingleton(typeof(IStaticTimeIntervalCache), typeof(StaticTimeIntervalCache));
        // services.AddSingleton(typeof(ITimeIntervalFactory), typeof(TimeIntervalFactory));
        // services.AddSingleton(typeof(IApplicationCache), typeof(ApplicationCache));
        //
        // //TODO: Get Values from configuration
        // var behaviourParams = new AisBehaviourParams();
        // services.AddSingleton(typeof(IBehaviourParameters), behaviourParams);
        return services;
    }
}