using Infra.Database;
using Microsoft.EntityFrameworkCore;
using Storage.Entities;
using Storage.Infra.Database.Models;
using Storage.Infra.Repositories.Contracts;

namespace Storage.Infra.Repositories;

public class RegistryRepository : BaseRepository<Registry, RegistryModel>, IRegistryRepository
{
  public RegistryRepository(DbContext dbContext) : base(dbContext)
  {
  }

  protected override RegistryModel ToDatabaseModel(Registry entity)
  {
    return new RegistryModel().FromEntity(entity);
  }
}