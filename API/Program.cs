var builder = WebApplication.CreateBuilder(args);

// Initialise Services
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();