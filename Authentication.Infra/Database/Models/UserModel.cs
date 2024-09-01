using Authentication.Entities;
using Primitives;
using Primitives.ValueObjects;

namespace Authentication.Infra.Database.Models;

public class UserModel : DatabaseModel<User>
{
  public string Name { get; set; } = String.Empty;
  
  public string Email { get; set; } = String.Empty;
  
  public string Password { get; set; } = String.Empty;
  
  public override User ToEntity()
  {
    return new User
    {
      Id = Id,
      
      Email = new Email(Email),
      
      Name = Name,
      
      Password = Password
    };
  }

  public override UserModel FromEntity(User entity)
  {
    return new UserModel
    {
      Email = entity.Email.Value,
      
      Name = entity.Name,
      
      Id = entity.Id,
      
      Password = entity.Password
    };
  }
}