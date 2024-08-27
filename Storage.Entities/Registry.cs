using Primitives;

namespace Storage.Entities;

public class Registry : Entity
{
  public Guid BookId { get; set; }
  
  public int Quantity { get; set; }
}