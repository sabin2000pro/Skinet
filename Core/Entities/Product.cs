namespace Core;
public class Product {
    public int Id { get; set; }
    public required string Name {get; set; }
    
    public required string Description {get; set; }

    public required int QuantityInStock {get; set; }
}