using Authentication.Entities;
using Primitives;

namespace Authentication.Infra.Database.Models;

public class RefreshTokenModel : DatabaseModel<RefreshToken>
{
  public Guid UserId { get; set; }
  
  public string Token { get; set; } = String.Empty;
  
  public DateTime ExpiresAt { get; set; }
  
  public override RefreshToken ToEntity()
  {
    return new RefreshToken
    {
      Id = Id,
      
      Token = Token,
      
      UserId = UserId,
      
      ExpiresAt = ExpiresAt
    };
  }

  public override RefreshTokenModel FromEntity(RefreshToken entity)
  {
    return new RefreshTokenModel
    {
      Id = entity.Id,
      
      Token = entity.Token,
      
      UserId = entity.UserId,
      
      ExpiresAt = entity.ExpiresAt
    };
  }
}