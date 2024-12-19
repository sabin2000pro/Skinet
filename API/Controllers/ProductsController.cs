using Core;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase {

    private readonly StoreContext context;

    public ProductsController(StoreContext _context)

    {
        context = _context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> FetchTasks() {
        return await context.Product.ToListAsync();
    }
    
    [HttpGet("{id:int}")] // api/products/2
    public async Task<ActionResult<Product>> GetProduct(int id) {
        
        var product = await context.Product.FindAsync(id);

        if(product == null) {
            return NotFound();
        }

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product) {
        context.Product.Add(product);

        await context.SaveChangesAsync();
        return product;
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product) { // API function that updates a single Product by ID

        if(product.Id != id || !ProductExists(id)) {
            return BadRequest("Cannot update this product");
        }

        context.Entry(product).State = EntityState.Modified;
        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id) {
        var product = await context.Product.FindAsync(id);

        if(product == null) {
            return NotFound();
        }

        context.Product.Remove(product);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductExists(int id) {
        return context.Product.Any(x => x.Id == id);
    }

}