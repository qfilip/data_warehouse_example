using DwHouse.DataAccess.Contexts;
using DwHouse.DataAccess.Entities.Store;
using DwHouse.Logic.Abstractions;
using DwHouse.Logic.Models;
using DwHouse.Logic.Utilities;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace DwHouse.Logic.Handlers.Store;

public static class CompleteOrder
{
    public class Handler : IAppHandler<Guid, Order>
    {
        private readonly StoreDbContext _dbContext;

        public Handler(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HandlerResult<Order>> Handle(Guid request)
        {
            var entity = await _dbContext.Orders
                .Where(x => x.Id == request)
                .FirstOrDefaultAsync();

            if (entity == null)
                return HandlerResult<Order>.NotFound();

            entity.CompletedOn = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

            return HandlerResult<Order>.Ok(entity);
        }

        public ValidationResult Validate(Guid request) => Validation.Empty;
    }
}
