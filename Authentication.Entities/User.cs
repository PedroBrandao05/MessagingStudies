using Primitives;
using Primitives.ValueObjects;

namespace Authentication.Entities;

public class User : Entity
{
  public string Name { get; set; } = String.Empty;
  
  public Email Email { get; set; } = Email.Empty();
}