using Lab6TestTask.Data;
using Lab6TestTask.Models;
using Lab6TestTask.Services.Interfaces;

namespace Lab6TestTask.Services.Implementations;

/// <summary>
/// WarehouseService.
/// Implement methods here.
/// </summary>
public class WarehouseService : IWarehouseService
{
    private readonly ApplicationDbContext _dbContext;

    public WarehouseService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Warehouse> GetWarehouseAsync()
    {
        DbSet<Warehouse> warehouses = (DbSet<Warehouse>)_dbContext.Warehouses;
        DbSet<Product> products = (DbSet<Product>)_dbContext.Products;

        var prods = from warehouse in warehouses
                    from product in products
                    where product.WarehouseId == warehouse.WarehouseId
                    where product.Status == Enums.ProductStatus.ReadyForDistribution
                    select product;
        
        var answer = warehouses.Where(w => w.WarehouseId == prods.OrderByDescending(p => p.Price * p.Quantity).First().WarehouseId).FirstAsync();
        
        return await answer;
    }

    public async Task<IEnumerable<Warehouse>> GetWarehousesAsync()
    {
        DbSet<Warehouse> warehouses = (DbSet<Warehouse>)_dbContext.Warehouses;
        DbSet<Product> products = (DbSet<Product>)_dbContext.Products;
        
        var answer = from warehouse in warehouses
                     from product in products
                     where product.ReceivedDate.Month == 4
                     || product.ReceivedDate.Month == 5
                     || product.ReceivedDate.Month == 6
                     && product.ReceivedDate.Year == 2025
                     where warehouse.WarehouseId == product.WarehouseId
                     select warehouse;
        
        return await answer.Distinct().ToListAsync();
    }
}
