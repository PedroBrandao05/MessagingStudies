using Primitives;

namespace Events;

public class ReservedBookEvent : Event
{
  public Guid BookId { get; set; }
}