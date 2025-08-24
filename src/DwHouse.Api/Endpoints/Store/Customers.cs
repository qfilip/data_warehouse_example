using DwHouse.Api.Services;
using DwHouse.Dtos.Store;
using DwHouse.Logic.Handlers.Store;
using Microsoft.AspNetCore.Mvc;

namespace DwHouse.Api.Endpoints.Store;

public class CreateCustomerEndpoint : IAppEndpoint
{
    public static async Task<IResult> Action(
        [FromBody] CreateCustomerDto dto,
        [FromServices] Pipeline pipeline,
        [FromServices] CreateCustomer.Handler handler) =>
            await pipeline.Pipe(handler, dto);

    public AppEndpoint GetEndpointInfo() =>
        new AppEndpoint(1, 0, EndpointMapper.Customers(""), eHttpMethod.Put, Action);
}
