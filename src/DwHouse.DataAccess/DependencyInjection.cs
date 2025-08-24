using DwHouse.DataAccess.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DwHouse.DataAccess;

public static class DependencyInjection
{
    public static void RegisterServices(WebApplicationBuilder builder)
    {
        var storeDb = Path.Combine(builder.Environment.WebRootPath, "store.db3");
        var warehouseDb = Path.Combine(builder.Environment.WebRootPath, "warehouse.db3");
        var prefix = "Datasource=";

        ConnectionStrings.Store = $"{prefix}{storeDb}";
        ConnectionStrings.Warehouse = $"{prefix}{warehouseDb}";

        builder.Services.AddDbContext<StoreDbContext>();
        builder.Services.AddDbContext<WarehouseDbContext>();
    }
}
