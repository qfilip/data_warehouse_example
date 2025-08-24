using DwHouse.Api.Services;

namespace DwHouse.Api;

internal static class DependencyInjection
{
    public static void RegisterAppServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<Pipeline>();
        
        DataAccess.DependencyInjection.RegisterServices(builder);
        Logic.DependencyInjection.RegisterServices(builder);
    }
}
