using DwHouse.DataAccess.Contexts;
using DwHouse.DataAccess.Entities.Store;
using DwHouse.Dtos.Store;
using DwHouse.Logic.Abstractions;
using DwHouse.Logic.Models;
using DwHouse.Logic.Utilities;
using FluentValidation.Results;

namespace DwHouse.Logic.Handlers.Store;

public static class CreateOrder
{
    public class Handler : IAppHandler<CreateOrderDto, Order>
    {
        private readonly StoreDbContext _dbContext;

        public Handler(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HandlerResult<Order>> Handle(CreateOrderDto request)
        {
            var entity = new Order
            {
                CustomerId = request.CustomerId,
                ProductId = request.ProductId,
                Amount = request.Amount,
                PlacedOn = DateTime.UtcNow
            };

            _dbContext.Orders.Add(entity);
            await _dbContext.SaveChangesAsync();

            return HandlerResult<Order>.Ok(entity);
        }

        public ValidationResult Validate(CreateOrderDto request) => Validation.Empty;
    }
}
