using System.Security.Cryptography;
using Authentication.Entities;
using Authentication.Infra.Repositories.Contracts;
using Authentication.Infra.Security.Contracts;
using Authentication.Infra.Security.Errors;

namespace Authentication.Infra.Security;

public class RefreshTokenService : IRefreshToken
{
  public IRefreshTokenRepository _refreshTokenRepository { get; }

  public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
  {
    _refreshTokenRepository = refreshTokenRepository;
  }
  
  public async Task<string> GenerateRefreshToken(Guid userId)
  {
    var candidate = await _refreshTokenRepository.GetByUserId(userId);

    if (candidate is not null)
    {
      await _refreshTokenRepository.Delete(candidate);
    }
    
    var randomNumber = new byte[32];
    using var rng = RandomNumberGenerator.Create();
    rng.GetBytes(randomNumber);
    
    var token = Convert.ToBase64String(randomNumber);

    var refreshToken = new RefreshToken
    {
      UserId = userId,

      Token = token
    };

    await _refreshTokenRepository.Create(refreshToken);

    return token;
  }

  public async Task ValidateRefreshToken(Guid userId)
  {
    var candidate = await _refreshTokenRepository.GetByUserId(userId);

    if (candidate is null || candidate.ExpiresAt < DateTime.Now)
    {
      throw new CouldNotRefreshToken();
    }

    await _refreshTokenRepository.Delete(candidate);
  }
}