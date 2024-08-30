using Primitives;

namespace BookRegistration.Entities;

public class Book : Entity
{
  public string Name { get; set; } = String.Empty;
  
  public string Description { get; set; } = String.Empty;
  
  public string? Thumbnail { get; set; } = String.Empty;
  
  public Guid? AuthorId { get; set; }
}