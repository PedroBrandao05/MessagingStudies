using Primitives;

namespace Authentication.Entities;

public class RefreshToken : Entity
{
  public Guid UserId { get; set; }
  
  public string Token { get; set; } = String.Empty;
  
  public DateTime ExpiresAt { get; set; } = DateTime.Now.AddDays(7);
}