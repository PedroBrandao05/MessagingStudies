using Microsoft.EntityFrameworkCore;

namespace BookRegistration.Infra.Database;

public class BookRegistrationDbContextFactory : IDbContextFactory<BookRegistrationDbContext>
{
  public BookRegistrationDbContext CreateDbContext()
  {
    var optionsBuilder = new DbContextOptionsBuilder<BookRegistrationDbContext>();

    var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING");
    
    optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));

    return new BookRegistrationDbContext(optionsBuilder.Options);
  }
}