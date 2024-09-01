using Authentication.Entities;
using Primitives;

namespace Authentication.Infra.Repositories.Contracts;

public interface IUserRepository : IRepository<User>
{
  public Task<User?> GetByEmail(string email);
}