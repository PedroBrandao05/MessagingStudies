using Events;
using MassTransit;
using Reservation.Infra.Repositories.Contracts;
using Reservation.Services.Contracts;

namespace Reservation.Services;

public class ReservationService : IReservationService
{
  private IReservationRepository _reservationRepository { get; }
  
  private IPublishEndpoint _publishEndpoint { get; }

  public ReservationService(IReservationRepository reservationRepository, IPublishEndpoint publishEndpoint)
  {
    _reservationRepository = reservationRepository;

    _publishEndpoint = publishEndpoint;
  }
  
  public async Task ReserveBook(Guid bookId)
  {
    var reservation = new Entities.Reservation
    {
      BookId = bookId
    };

    await _reservationRepository.Create(reservation);

    await _publishEndpoint.Publish(new ReservedBookEvent
    {
      BookId = bookId
    });
  }
}