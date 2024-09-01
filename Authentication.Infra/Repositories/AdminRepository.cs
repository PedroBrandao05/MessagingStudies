using Authentication.Entities;
using Authentication.Infra.Database.Models;
using Authentication.Infra.Repositories.Contracts;
using Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infra.Repositories;

public class AdminRepository : BaseRepository<Admin, AdminModel>, IAdminRepository
{
  public AdminRepository(DbContext dbContext) : base(dbContext)
  {
  }

  protected override AdminModel ToDatabaseModel(Admin entity)
  {
    return new AdminModel().FromEntity(entity);
  }

  public Task<Admin?> GetByEmail(string email)
  {
    var admin = _dbSet.Where(a => a.Email == email).ToList().FirstOrDefault();

    return Task.FromResult(admin?.ToEntity());
  }
}