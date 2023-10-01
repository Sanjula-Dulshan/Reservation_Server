using Microsoft.AspNetCore.Mvc;
using Reservation_Server.Models.Reservations;
using Reservation_Server.Services.Reservations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Reservation_Server.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService reservationService;

        public ReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;

        }

        [HttpGet]
        public ActionResult<List<Reservation>> Get()
        {
            return reservationService.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<Reservation> Get(string id)
        {
            var reservation = reservationService.Get(id);

            if (reservation == null)
            {
                return NotFound("Reservation not found");
            }
            return reservation;
        }


        [HttpPost]
        public ActionResult<Reservation> Post([FromBody] Reservation reservation)
        {
            var result = reservationService.Create(reservation);

            return Ok(result);
        }


        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Reservation reservation)
        {
            var existingReservation = reservationService.Get(id);

            if (existingReservation == null)
            {
                return NotFound("Reservation not found");
            }

            var result = reservationService.Update(id, reservation);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {

            var reservation = reservationService.Get(id);

            if (reservation == null)
            {
                return NotFound("Reservation not found");
            }
            reservationService.Delete(id);

            return Ok("Reservation deleted");
        }

    }
}
