using DwHouse.Logic.Models;

namespace DwHouse.Logic.Abstractions;

public interface IAppEventHandler<TRequest, TResponse>
{
    Task<HandlerResult<TResponse>> Handle(TRequest request);
}
