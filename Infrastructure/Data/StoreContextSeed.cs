using System.Text.Json;
using Core;

namespace Infrastructure;
public class StoreContextSeed {

    public static async Task SeedAsync(StoreContext context) {

        // Only seed the data if we don't have any already..
    if(!context.Product.Any()) {
        var productsData = await File.ReadAllTextAsync("/Users/sabin2000pro/Documents/Skinet/Infrastructure/Data/SeedData/products.json");
        var products = JsonSerializer.Deserialize<List<Product>>(productsData);

        if(products == null) {
            return;
        }

        context.Product.AddRange(products);
        await context.SaveChangesAsync();

    }

    }

}