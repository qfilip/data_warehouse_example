using DwHouse.Logic.Models;
using FluentValidation.Results;

namespace DwHouse.Logic.Abstractions;

public interface IAppHandler<TRequest, TResponse>
{
    ValidationResult Validate(TRequest request);
    Task<HandlerResult<TResponse>> Handle(TRequest request);
}
