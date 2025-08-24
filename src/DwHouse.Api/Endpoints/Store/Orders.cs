using DwHouse.Api.Services;
using DwHouse.Dtos.Store;
using DwHouse.Logic.Handlers.Store;
using Microsoft.AspNetCore.Mvc;

namespace DwHouse.Api.Endpoints.Store;

public class CreateOrderEndpoint : IAppEndpoint
{
    public static async Task<IResult> Action(
        [FromBody] CreateOrderDto dto,
        [FromServices] Pipeline pipeline,
        [FromServices] CreateOrder.Handler handler) =>
            await pipeline.Pipe(handler, dto);

    public AppEndpoint GetEndpointInfo() =>
        new AppEndpoint(1, 0, EndpointMapper.Orders(""), eHttpMethod.Put, Action);
}

public class CompleteOrderEndpoint : IAppEndpoint
{
    public static async Task<IResult> Action(
        [FromBody] CompleteOrderDto dto,
        [FromServices] Pipeline pipeline,
        [FromServices] CompleteOrder.Handler handler) =>
            await pipeline.Pipe(handler, dto.OrderId);

    public AppEndpoint GetEndpointInfo() =>
        new AppEndpoint(1, 0, EndpointMapper.Orders(""), eHttpMethod.Patch, Action);
}
