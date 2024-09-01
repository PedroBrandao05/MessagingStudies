using Microsoft.EntityFrameworkCore;

namespace Authentication.Infra.Database;

public class AuthenticationDbContextFactory : IDbContextFactory<AuthenticationDbContext>
{
  public AuthenticationDbContext CreateDbContext()
  {
    var optionsBuilder = new DbContextOptionsBuilder<AuthenticationDbContext>();

    var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING");
    
    optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));

    return new AuthenticationDbContext(optionsBuilder.Options);
  }
}