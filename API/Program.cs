using Core;
using Core.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Initialise Services
builder.Services.AddControllers();

// 1. Initialise the Db Context as Middleware
builder.Services.AddDbContext<StoreContext>(options => {
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// 2. Initialize the Repository Pattern & Generic as Services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var app = builder.Build();
app.UseAuthorization();

app.MapControllers();

// 3. Set up the Middleware for Seeding the Data into the database
try {

   using var scope = app.Services.CreateScope();
   var services = scope.ServiceProvider;
   var context = services.GetRequiredService<StoreContext>();

   await context.Database.MigrateAsync();
   await StoreContextSeed.SeedAsync(context);
}

catch(Exception exc) {
   Console.WriteLine(exc.Message);
}

app.Run();