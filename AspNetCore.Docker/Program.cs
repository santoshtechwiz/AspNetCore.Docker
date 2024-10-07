using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from appsettings.json
var configuration = builder.Configuration;
configuration.AddJsonFile("appsettings.json");
// Add PostgreSQL DbContext using the connection string from appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));

// Add Redis cache service with configuration from appsettings.json
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = configuration.GetValue<string>("Redis:Configuration");  // Get Redis configuration from appsettings.json
    options.InstanceName = configuration.GetValue<string>("Redis:InstanceName");  // Get Redis instance name from appsettings.json
});

// Add controllers
builder.Services.AddControllers();

// Add HttpClient
builder.Services.AddHttpClient();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();  // Ensures the database is created if it doesn't exist
}
// Configure middleware
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
