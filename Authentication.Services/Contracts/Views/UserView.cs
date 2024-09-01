namespace Authentication.Services.Contracts.Responses;

public class UserView
{
  public Guid Id { get; set; } = Guid.NewGuid();
  
  public string Name { get; set; } = String.Empty;
  
  public string Email { get; set; } = String.Empty;
  
  public Guid? CompanyId { get; set; }
}