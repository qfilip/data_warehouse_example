using DwHouse.Api.Endpoints;
using DwHouse.Api.Middlewares;
using Microsoft.AspNetCore.Http.Json;

namespace DwHouse.Api.Static;

public class Webapi
{
    public static WebApplication Build(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<JsonOptions>(x =>
        {
            // don't return recursive types
            // ReferenceHandler adds $id property
            // x.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        });

        builder.Services.AddCors(b => b.AddDefaultPolicy(o => o.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

        AppInitializer.LoadAuthConfiguration(builder.Configuration);
        DependencyInjection.RegisterAppServices(builder);

        builder.Host.UseDefaultServiceProvider((_, options) =>
        {
            options.ValidateOnBuild = true;
            options.ValidateScopes = true;
        });

        return builder.Build();
    }

    public static void Run(WebApplication app)
    {
        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseCors();

        EndpointMapper.Map(app);

        app.UseMiddleware<AppHttpMiddleware>();

        app.Run();
    }
}
