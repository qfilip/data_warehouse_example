using DwHouse.Api.Services;
using DwHouse.Dtos.Store;
using DwHouse.Logic.Handlers.Store;
using Microsoft.AspNetCore.Mvc;

namespace DwHouse.Api.Endpoints.Store;

public class CreateProductEndpoint : IAppEndpoint
{
    public static async Task<IResult> Action(
        [FromBody] CreateProductDto dto,
        [FromServices] Pipeline pipeline,
        [FromServices] CreateProduct.Handler handler) =>
            await pipeline.Pipe(handler, dto);

    public AppEndpoint GetEndpointInfo() =>
        new AppEndpoint(1, 0, EndpointMapper.Products(""), eHttpMethod.Put, Action);
}
