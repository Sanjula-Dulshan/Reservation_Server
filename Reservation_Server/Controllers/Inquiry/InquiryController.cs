using Microsoft.AspNetCore.Mvc;
using Reservation_Server.Models.Inquiries;
using Reservation_Server.Services.Inquiries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Reservation_Server.Controllers.Inquiries
{
    [Route("api/[controller]")]
    [ApiController]
    public class InquiryController : ControllerBase
    {
        private readonly IInquiryService inquiryService;

        public InquiryController(IInquiryService inquiryService)
        {
            this.inquiryService = inquiryService;
        }

        // GET: api/<InquiryController>
        [HttpGet]
        public ActionResult<List<Inquiry>> Get()
        {
            return inquiryService.Get();
        }

        // GET api/<InquiryController>/5
        [HttpGet("{id}")]
        public ActionResult<Inquiry> Get(String id)
        {
            Inquiry inquiry = inquiryService.Get(id);

            if (inquiry == null)
            {
                return NotFound($"Inquiry  not found ");
            }

            return inquiry;
        }

        // POST api/<InquiryController>
        [HttpPost]
        public ActionResult<Inquiry> Post([FromBody] Inquiry inquiry)
        {
            Inquiry result = inquiryService.Create(inquiry);

            if (result == null)
            {
                return BadRequest("Inquiry not created");
            }
            else
            {
                return Ok(result);
            }
        }

        // PUT api/<InquiryController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Inquiry inquiry)
        {
            var existingInquiry = inquiryService.Get(id);

            if (existingInquiry == null)
            {
                return NotFound($"Inquiry with id = {id} not found ");
            }

            inquiryService.Update(id, inquiry);
            return Ok("Updated");
        }

        // DELETE api/<InquiryController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            Inquiry inquiry = inquiryService.Get(id);

            if (inquiry == null)
            {
                return NotFound($"Inquiry  not found ");
            }

            inquiryService.Delete(inquiry.Id);
            return Ok("Inquiry deleted");
        }
    }
}

