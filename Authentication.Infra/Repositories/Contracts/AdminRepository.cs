using Authentication.Entities;
using Primitives;

namespace Authentication.Infra.Repositories.Contracts;

public interface IAdminRepository : IRepository<Admin>
{
  public Task<Admin?> GetByEmail(string email);
}