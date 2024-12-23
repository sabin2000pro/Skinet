using Core;
using Microsoft.AspNetCore.Mvc;

namespace API;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository productsRepo) : ControllerBase {

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsAsync() {
        return Ok(await productsRepo.GetProductsAsync());
    }
    
    [HttpGet("{id:int}")] // api/products/2
    public async Task<ActionResult<Product>> GetProduct(int id) {

        var product = await productsRepo.GetProductByIdAsync(id);

        // Check to see if the Product Exists
        if(product == null) {
            return NotFound();
        }

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product) {
        productsRepo.AddProduct(product);

        // Check to see if changes are made
        if(await productsRepo.SaveChangesAsync()) {
            return CreatedAtAction("Get Product", new {id = product.Id}, product);
        }

        return BadRequest("Problem creating Product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product) { // API function that updates a single Product by ID

        if(product.Id != id || !ProductExists(id)) {
            return BadRequest("Cannot update this product");
        }

        productsRepo.UpdateProduct(product);

        if(await productsRepo.SaveChangesAsync()) {
            return NoContent();
        }

        return BadRequest("There was a problem updating the product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id) {
        var product = await productsRepo.GetProductByIdAsync(id);

        if(product == null) {
            return NotFound();
        }

        productsRepo.DeleteProduct(product);

        if(await productsRepo.SaveChangesAsync()) {
            return NoContent();
        }

        return BadRequest("There was a problem deleting the product..");
    }

    private bool ProductExists(int id) {
        return productsRepo.CheckProductExistance(id);
    }

}