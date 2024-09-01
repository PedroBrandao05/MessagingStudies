using Authentication.Entities;
using Authentication.Infra.Database.Models;
using Authentication.Infra.Repositories.Contracts;
using Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infra.Repositories;

public class UserRepository : BaseRepository<User, UserModel>, IUserRepository
{
  public UserRepository(DbContext dbContext) : base(dbContext)
  {
  }

  protected override UserModel ToDatabaseModel(User entity)
  {
    return new UserModel().FromEntity(entity);
  }

  public Task<User?> GetByEmail(string email)
  {
    var user = _dbSet.Where(u => u.Email == email).ToList().FirstOrDefault();
    
    return Task.FromResult(user?.ToEntity());
  }
}