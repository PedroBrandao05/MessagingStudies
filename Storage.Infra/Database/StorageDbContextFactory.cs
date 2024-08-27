using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Storage.Infra.Database;

public class StorageDbContextFactory : IDesignTimeDbContextFactory<StorageDbContext>
{
  public StorageDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<StorageDbContext>();

    var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING");
    
    optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));

    return new StorageDbContext(optionsBuilder.Options);
  }
}