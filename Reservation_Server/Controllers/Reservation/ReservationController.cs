/*
    Sri Lanka Institute of Information Technology
    Year  :  4th Year 2nd Semester
    Module Code  :  SE4040
    Module  :  Enterprise Application Development
    Contributor  :  IT20253530, IT20240042, IT20140120, IT20265892
*/

using Microsoft.AspNetCore.Mvc;
using Reservation_Server.Models.Reservations;
using Reservation_Server.Services.Reservations;


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
