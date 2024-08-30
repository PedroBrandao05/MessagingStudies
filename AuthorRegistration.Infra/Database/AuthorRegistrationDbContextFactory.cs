using Microsoft.EntityFrameworkCore;

namespace AuthorRegistration.Infra.Database;

public class AuthorRegistrationDbContextFactory : IDbContextFactory<AuthorRegistrationDbContext>
{
  public AuthorRegistrationDbContext CreateDbContext()
  {
    var optionsBuilder = new DbContextOptionsBuilder<AuthorRegistrationDbContext>();

    var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING");
    
    optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));

    return new AuthorRegistrationDbContext(optionsBuilder.Options);
  }
}