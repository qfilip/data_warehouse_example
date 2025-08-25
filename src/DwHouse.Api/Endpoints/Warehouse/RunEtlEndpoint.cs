using DwHouse.Api.Services;
using DwHouse.Logic.Handlers.Warehouse;
using DwHouse.Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace DwHouse.Api.Endpoints.Warehouse;

public class RunEtlEndpoint : IAppEndpoint
{
    public static async Task<IResult> Action(
        [FromServices] Pipeline pipeline,
        [FromServices] RunETL.Handler handler) =>
            await pipeline.Pipe(handler, Unit.New);

    public AppEndpoint GetEndpointInfo() =>
        new AppEndpoint(1, 0, EndpointMapper.ETL(""), eHttpMethod.Post, Action);
}