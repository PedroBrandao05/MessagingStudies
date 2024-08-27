using Microsoft.EntityFrameworkCore;
using Storage.Infra.Database.Models;

namespace Storage.Infra.Database;

public class StorageDbContext : DbContext
{
  public DbSet<RegistryModel> Registries { get; set; }

  public StorageDbContext(DbContextOptions<StorageDbContext> options) : base(options)
  {
  }
}