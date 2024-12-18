using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions options) : base(options) {}
    
}