using Lab6TestTask.Data;
using Lab6TestTask.Models;
using Lab6TestTask.Services.Interfaces;

namespace Lab6TestTask.Services.Implementations;

/// <summary>
/// ProductService.
/// Implement methods here.
/// </summary>
public class ProductService : IProductService
{
    private readonly ApplicationDbContext _dbContext;

    public ProductService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product> GetProductAsync()
    {
        DbSet<Product> products = (DbSet<Product>)_dbContext.Products;
        var mostExpensive = products.OrderByDescending(p => p.Price).FirstAsync();
        return await mostExpensive;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        DbSet<Product> products = (DbSet<Product>)_dbContext.Products;
        var prods = products.Where(p => p.ReceivedDate.Year == 2025 && p.Quantity > 1000);
        return await prods.ToListAsync();
    }
}
