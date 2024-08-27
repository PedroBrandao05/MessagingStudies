using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Reservation.Infra.Database;

public class ReservationDbContextFactory : IDesignTimeDbContextFactory<ReservationDbContext>
{
  public ReservationDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<ReservationDbContext>();

    var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING");
    
    optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));

    return new ReservationDbContext(optionsBuilder.Options);
  }
}