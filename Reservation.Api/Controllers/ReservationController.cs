using Microsoft.AspNetCore.Mvc;
using Reservation.Services.Contracts;

namespace Reservation.Api.Controllers;

[Route("reservation")]
public class ReservationController : ControllerBase
{
  private IReservationService _reservationService { get; }

  public ReservationController(IReservationService reservationService)
  {
    _reservationService = reservationService;
  }

  [HttpPost("{bookId}")]
  public async Task<ActionResult> ReserveBook([FromRoute] string bookId)
  {
    await _reservationService.ReserveBook(new Guid(bookId));

    return Created();
  }
}