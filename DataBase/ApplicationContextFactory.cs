using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataBase; 

public class ApplicationContextFactory: IDesignTimeDbContextFactory<ApplicationContext> {
    public ApplicationContext CreateDbContext(string[]? args = null) {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("private.json");
        var config = builder.Build();
        var connectionString = config.GetConnectionString("DefaultConnection");
        
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder.UseNpgsql(connectionString);
        
        return new ApplicationContext(optionsBuilder.Options);
    }
}