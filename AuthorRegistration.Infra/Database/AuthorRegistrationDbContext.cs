using AuthorRegistration.Infra.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorRegistration.Infra.Database;

public class AuthorRegistrationDbContext : DbContext
{
  public DbSet<AuthorModel> Author { get; }

  public AuthorRegistrationDbContext(DbContextOptions<AuthorRegistrationDbContext> options) : base(options)
  {
  }
}