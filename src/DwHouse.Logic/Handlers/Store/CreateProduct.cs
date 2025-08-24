using DwHouse.DataAccess.Contexts;
using DwHouse.DataAccess.Entities.Store;
using DwHouse.Dtos.Store;
using DwHouse.Logic.Abstractions;
using DwHouse.Logic.Models;
using DwHouse.Logic.Utilities;
using FluentValidation.Results;

namespace DwHouse.Logic.Handlers.Store;

public static class CreateProduct
{
    public class Handler : IAppHandler<CreateProductDto, Product>
    {
        private readonly StoreDbContext _dbContext;

        public Handler(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HandlerResult<Product>> Handle(CreateProductDto request)
        {
            var entity = new Product
            {
                Name = request.Name
            };

            _dbContext.Products.Add(entity);
            await _dbContext.SaveChangesAsync();

            return HandlerResult<Product>.Ok(entity);
        }

        public ValidationResult Validate(CreateProductDto request) => Validation.Empty;
    }
}
