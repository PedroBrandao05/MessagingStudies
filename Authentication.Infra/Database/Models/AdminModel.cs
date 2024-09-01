using Authentication.Entities;
using Primitives;
using Primitives.ValueObjects;

namespace Authentication.Infra.Database.Models;

public class AdminModel : DatabaseModel<Admin>
{
  public string Name { get; set; } = String.Empty;
  
  public string Email { get; set; } = String.Empty;
  
  public Guid CompanyId { get; set; }

  public string Password { get; set; } = String.Empty;
  
  public override Admin ToEntity()
  {
    return new Admin
    {
      Id = Id,
      
      Email = new Email(Email),
      
      Name = Name,
      
      Password = Password,
      
      CompanyId = CompanyId
    };
  }

  public override AdminModel FromEntity(Admin entity)
  {
    return new AdminModel
    {
      Id = entity.Id,
      
      Email = entity.Email.Value,
      
      Name = entity.Name,
      
      Password = entity.Password,
      
      CompanyId = entity.CompanyId
    };
  }
}