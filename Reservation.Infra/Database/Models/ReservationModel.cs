using Primitives;

namespace Reservation.Infra.Database.Models;

public class ReservationModel : DatabaseModel<Entities.Reservation>
{
  public Guid BookId { get; set; }
  
  public override Entities.Reservation ToEntity()
  {
    return new Entities.Reservation
    {
      Id = Id,

      BookId = BookId
    };
  }

  public override ReservationModel FromEntity(Entities.Reservation entity)
  {
    return new ReservationModel
    {
      Id = entity.Id,

      BookId = entity.BookId
    };
  }
}