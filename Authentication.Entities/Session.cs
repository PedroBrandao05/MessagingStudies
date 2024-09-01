namespace Authentication.Entities;

public class Session
{
  public Guid UserId { get; set; }
  
  public string Email { get; set; }
  
  public Guid? CompanyId { get; set; }
}