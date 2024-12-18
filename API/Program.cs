using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Initialise Services
builder.Services.AddControllers();

// 1. Initialise the Db Context as Middleware
builder.Services.AddDbContext<StoreContext>(options => {
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();
app.UseAuthorization();

app.MapControllers();
app.Run();