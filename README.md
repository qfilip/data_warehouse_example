# DwHouse

## Database migrations
Run from DwHouse.DataAccess:
```
Add-Migration DwInitial -Context WarehouseDbContext -OutputDir ./Migrations/Warehouse
Add-Migration StoreInitial -Context StoreDbContext -OutputDir ./Migrations/Store

Update-Database -Context WarehouseDbContext
Update-Database -Context StoreDbContext
```