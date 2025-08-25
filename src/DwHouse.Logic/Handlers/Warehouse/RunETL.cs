using DwHouse.DataAccess.Abstractions;
using DwHouse.DataAccess.Contexts;
using DwHouse.DataAccess.Entities.Store;
using DwHouse.DataAccess.Entities.Warehouse;
using DwHouse.Logic.Abstractions;
using DwHouse.Logic.Models;
using DwHouse.Logic.Utilities;
using FluentValidation.Results;
using MassTransit.Initializers;
using Microsoft.EntityFrameworkCore;

namespace DwHouse.Logic.Handlers.Warehouse;
public static class RunETL
{
    public class Handler : IAppHandler<Unit, int>
    {
        private readonly StoreDbContext _storeDb;
        private readonly WarehouseDbContext _whouseDb;

        public Handler(StoreDbContext storeDb, WarehouseDbContext whouseDb)
        {
            _storeDb = storeDb;
            _whouseDb = whouseDb;
        }

        public async Task<HandlerResult<int>> Handle(Unit request)
        {
            var audit = await _whouseDb.Facts
                .Select(x => x.AuditRecord)
                .OrderBy(x => x.CreatedAt)
                .AsNoTracking()
                .LastOrDefaultAsync();
            
            var lastDate = audit?.CreatedAt ?? DateTime.MinValue;

            var orders = await _storeDb.Orders
                .Where(x =>
                    x.PlacedOn >= lastDate ||
                    x.CompletedOn >= lastDate
                )
                .Include(x => x.Product)
                .Include(x => x.Customer)
                .AsNoTracking()
                .ToArrayAsync();

            await InsertNewRecordsAsync(
                    orders, _whouseDb.Customers, x => x.Customer!,
                    x => new CustomerDimension { Id = x.Id, Name = x.Name});
            
            await InsertNewRecordsAsync(
                orders, _whouseDb.Products, x => x.Product!,
                x => new ProductDimension { Id = x.Id, Name = x.Name });

            var orderDimensions = orders.Select(x => new OrderDimension
            {
                Id = x.Id,
                Amount = x.Amount,
                PlacedOn = x.PlacedOn,
                CompletedOn = x.CompletedOn,
            });

            _whouseDb.Orders.AddRange(orderDimensions);

            await _whouseDb.SaveChangesAsync();

            foreach (var order in orders)
            {
                var fact = new Fact
                {
                    OrderId = order.Id,
                    CustomerId = order.CustomerId,
                    ProductId = order.ProductId,
                    PlacedOn = order.PlacedOn,
                    CompletedOn = order.CompletedOn
                };

                _whouseDb.Facts.Add(fact);
            }

            await _whouseDb.SaveChangesAsync();

            return HandlerResult<int>.Ok(orders.Length);
        }

        private async Task InsertNewRecordsAsync<T, U>(
            Order[] orders,
            DbSet<U> dbSet,
            Func<Order, T> selector,
            Func<T, U> mapper)
            where T : class, IId<Guid>
            where U : class, IId<Guid>
        {
            var records = orders
                .Select(selector)
                .DistinctBy(x => x.Id)
                .ToArray();

            var recordIds = records.Select(x => x!.Id).ToArray();

            if(await dbSet.Select(x => x.Id).CountAsync() == 0)
            {
                var all = records
                .Select(mapper)
                .ToArray();

                dbSet.AddRange(all);
                await _whouseDb.SaveChangesAsync();
            }

            var newRecordIds = await dbSet
                .Where(x => !recordIds.Contains(x.Id))
                .Select(x => x.Id)
                .ToArrayAsync();

            var recordsToInsert = records
                .Where(x => newRecordIds.Contains(x!.Id))
                .Select(mapper)
                .ToArray();

            dbSet.AddRange(recordsToInsert);
        }

        public ValidationResult Validate(Unit request) => Validation.Empty;
    }
}
