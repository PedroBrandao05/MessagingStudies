namespace Reservation.Services.Contracts;

public interface IReservationService
{
  public Task ReserveBook(Guid bookId);
}