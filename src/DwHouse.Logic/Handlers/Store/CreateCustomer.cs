using DwHouse.DataAccess.Contexts;
using DwHouse.DataAccess.Entities.Store;
using DwHouse.Dtos.Store;
using DwHouse.Logic.Abstractions;
using DwHouse.Logic.Models;
using DwHouse.Logic.Utilities;
using FluentValidation.Results;

namespace DwHouse.Logic.Handlers.Store;

public static class CreateCustomer
{
    public class Handler : IAppHandler<CreateCustomerDto, Customer>
    {
        private readonly StoreDbContext _dbContext;

        public Handler(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HandlerResult<Customer>> Handle(CreateCustomerDto request)
        {
            var entity = new Customer
            {
                Name = request.Name
            };

            _dbContext.Customers.Add(entity);
            await _dbContext.SaveChangesAsync();

            return HandlerResult<Customer>.Ok(entity);
        }

        public ValidationResult Validate(CreateCustomerDto request) => Validation.Empty;
    }
}
