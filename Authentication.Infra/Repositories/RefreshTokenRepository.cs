using Authentication.Entities;
using Authentication.Infra.Database.Models;
using Authentication.Infra.Repositories.Contracts;
using Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infra.Repositories;

public class RefreshTokenRepository : BaseRepository<RefreshToken, RefreshTokenModel>, IRefreshTokenRepository
{
  public RefreshTokenRepository(DbContext dbContext) : base(dbContext)
  {
  }

  protected override RefreshTokenModel ToDatabaseModel(RefreshToken entity)
  {
    return new RefreshTokenModel().FromEntity(entity);
  }

  public Task<RefreshToken?> GetByUserId(Guid userId)
  {
    var refreshToken = _dbSet.Where(r => r.UserId == userId).ToList().FirstOrDefault();

    return Task.FromResult(refreshToken?.ToEntity());
  }
}