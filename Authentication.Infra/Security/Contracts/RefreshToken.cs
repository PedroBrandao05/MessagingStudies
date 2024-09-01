namespace Authentication.Infra.Security.Contracts;

public interface IRefreshToken
{
  public Task<string> GenerateRefreshToken(Guid userId);

  public Task ValidateRefreshToken(Guid userId);
}