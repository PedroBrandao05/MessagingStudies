using Primitives;

namespace AuthorRegistration.Entities;

public class Author : Entity
{
  public string Name { get; set; } = String.Empty;
  
  public string? Picture { get; set; }
  
  public string? Nationality { get; set; }
  
  public string? Biography { get; set; }
}