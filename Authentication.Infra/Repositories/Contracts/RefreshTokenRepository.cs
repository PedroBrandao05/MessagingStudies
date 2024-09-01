using Authentication.Entities;
using Primitives;

namespace Authentication.Infra.Repositories.Contracts;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
  public Task<RefreshToken?> GetByUserId(Guid userId);
}