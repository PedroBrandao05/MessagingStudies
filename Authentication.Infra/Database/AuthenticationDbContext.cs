using Authentication.Infra.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infra.Database;

public class AuthenticationDbContext : DbContext
{
  public DbSet<UserModel> User { get; }
  
  public DbSet<AdminModel> Admin { get; }
  
  public DbSet<RefreshTokenModel> RefreshToken { get; }

  public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
  {
  }
}