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
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> FetchTasks() {
        return await context.Product.ToListAsync();
    }
    

}