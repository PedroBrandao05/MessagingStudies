using Microsoft.EntityFrameworkCore;
using Reservation.Infra.Database.Models;

namespace Reservation.Infra.Database;

public class ReservationDbContext : DbContext
{
  public DbSet<ReservationModel> Reservations { get; set; }

  public ReservationDbContext(DbContextOptions<ReservationDbContext> options) : base(options)
  {
  }
}