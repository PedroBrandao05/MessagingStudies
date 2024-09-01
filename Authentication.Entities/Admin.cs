namespace Authentication.Entities;

public class Admin : User
{
  public Guid CompanyId { get; set; } = Guid.NewGuid();
}