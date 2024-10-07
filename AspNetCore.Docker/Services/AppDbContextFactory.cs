using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args = null)
    {
        // Set up configuration sources.
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Ensure this path points to your project root
            .AddJsonFile("appsettings.json") // Ensure this file exists in the project root
            .Build();

        var builder = new DbContextOptionsBuilder<AppDbContext>();

        // Get connection string from appsettings.json
        var connectionString = configuration.GetConnectionString("PostgresConnection");
        builder.UseNpgsql(connectionString);

        return new AppDbContext(builder.Options);
    }
}
