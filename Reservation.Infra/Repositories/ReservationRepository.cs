using Infra.Database;
using Microsoft.EntityFrameworkCore;
using Reservation.Infra.Database.Models;
using Reservation.Infra.Repositories.Contracts;

namespace Reservation.Infra.Repositories;

public class ReservationRepository : BaseRepository<Entities.Reservation, ReservationModel>, IReservationRepository
{
  public ReservationRepository(DbContext dbContext) : base(dbContext)
  {
  }

  protected override ReservationModel ToDatabaseModel(Entities.Reservation entity)
  {
    return new ReservationModel().FromEntity(entity);
  }
}