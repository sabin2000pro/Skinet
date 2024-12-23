using Core;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Initialise Services
builder.Services.AddControllers();

// 1. Initialise the Db Context as Middleware
builder.Services.AddDbContext<StoreContext>(options => {
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// 2. Initialize the Repository as a Service
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();
app.UseAuthorization();

app.MapControllers();
app.Run();