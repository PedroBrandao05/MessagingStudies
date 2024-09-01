using Authentication.Entities;

namespace Authentication.Infra.Security.Contracts;

public interface IJwt
{
  public string GenerateToken(User user, Guid? companyId);

  public Session Decode(string token);
}