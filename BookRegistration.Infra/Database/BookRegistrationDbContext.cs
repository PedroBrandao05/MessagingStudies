using BookRegistration.Infra.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BookRegistration.Infra.Database;

public class BookRegistrationDbContext : DbContext
{
  public DbSet<BookModel> Book { get; }

  public BookRegistrationDbContext(DbContextOptions<BookRegistrationDbContext> options) : base(options)
  {
  }
}