using DwHouse.Logic.Abstractions;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DwHouse.Logic;

public static class DependencyInjection
{
    public static void RegisterServices(WebApplicationBuilder builder)
    {
        var handlerTypes = new Type[] { typeof(IAppHandler<,>), typeof(IAppEventHandler<,>) };
        foreach (var handlerType in handlerTypes)
        {
            RegisterHandlers(handlerType, builder.Services);
            RegisterValidators(builder.Services);
        }
    }

    private static void RegisterHandlers(Type handlerType, IServiceCollection services)
    {
        var interfaceName = handlerType.Name;
        var registerAction = (Type x) =>
        {
            services.AddTransient(x);
        };

        RegisterDynamically(interfaceName, registerAction);
    }

    private static void RegisterValidators(IServiceCollection services)
    {
        var interfaceName = typeof(IValidator).Name;
        var registerAction = (Type x) =>
        {
            services.AddSingleton(x);
        };

        RegisterDynamically(interfaceName, registerAction);
    }

    private static void RegisterDynamically(string interfaceName, Action<Type> registerAction)
    {
        var assembly = typeof(IAssemblyMarker).Assembly;

        var serviceTypes = assembly
        .GetTypes()
        .Where(x =>
            x.GetInterface(interfaceName) != null &&
            !x.IsAbstract &&
            !x.IsInterface)
        .ToList();

        serviceTypes.ForEach(registerAction);
    }
}
