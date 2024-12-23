using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public class ProductRepository : IProductRepository

{
    private readonly StoreContext _context;

    public ProductRepository(StoreContext context) {
        _context = context;
    }

    public void AddProduct(Product product)

    {
        _context.Product.Add(product);
    }

    public void DeleteProduct(Product product) // Deletes a Single Product

    {
       _context.Product.Remove(product);
    }

    public async Task<Product?> GetProductByIdAsync(int id)

    {
        return await _context.Product.FindAsync(id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()

    {
        return await _context.Product.ToListAsync();
    }

    public bool CheckProductExistance(int id)

    {
        return _context.Product.Any(x => x.Id == id);
    }

    public async Task<bool> SaveChangesAsync()

    {   
        return await _context.SaveChangesAsync() > 0;
    }

    public void UpdateProduct(Product product)

    {
        _context.Entry(product).State = EntityState.Modified;
    }

    public async Task<IReadOnlyList<string>> GetBrandsAsync() // Returns the Brand(s) that belongs to a Product
    {
       return await _context.Product.Select(x => x.Brand).Distinct().ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetTypesAsync() // Returns the Types that belongs to a Product
    {
        return await _context.Product.Select(x => x.Type).Distinct().ToListAsync();
    }

}