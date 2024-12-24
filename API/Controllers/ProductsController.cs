using Core;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IGenericRepository<Product> productsRepo) : ControllerBase {

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsAsync(string? brand, 
    string? type, string? sort) {
        return Ok(await productsRepo.ListAllAsync());
    }
    
    [HttpGet("{id:int}")] // api/products/2
    public async Task<ActionResult<Product>> GetProduct(int id) {

        var product = await productsRepo.GetByIdAsync(id);

        // Check to see if the Product Exists
        if(product == null) {
            return NotFound();
        }

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product) {
        productsRepo.Add(product);

        // Check to see if changes are made
        if(await productsRepo.SaveAllAsync()) {
            return CreatedAtAction("Get Product", new {id = product.Id}, product);
        }

        return BadRequest("Problem creating Product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product) { // API function that updates a single Product by ID

        if(product.Id != id || !ProductExists(id)) {
            return BadRequest("Cannot update this product");
        }

        productsRepo.Update(product);

        if(await productsRepo.SaveAllAsync()) {
            return NoContent();
        }

        return BadRequest("There was a problem updating the product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id) {
        var product = await productsRepo.GetByIdAsync(id);

        if(product == null) {
            return NotFound();
        }

        productsRepo.Remove(product);

        if(await productsRepo.SaveAllAsync()) {
            return NoContent();
        }

        return BadRequest("There was a problem deleting the product..");
    }

    [HttpGet("brands")] // TODO: Implement method
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands() { // Returns a list of Brands corresponding to a Product
        return Ok();
    }

    [HttpGet("types")] // TODO: Implement Method
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes() {
        return Ok();
    }

    private bool ProductExists(int id) {
        return productsRepo.Exists(id);
    }

}