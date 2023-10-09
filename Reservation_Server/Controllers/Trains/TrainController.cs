using Reservation_Server.Models.TrainModel;
using Reservation_Server.Services.TrainService;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Reservation_Server.Controllers.TrainController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainController : ControllerBase
    {
        private readonly ITrainService trainService;

        public TrainController(ITrainService trainService)
        {
            this.trainService = trainService;
        }

        // GET: api/<TrainController>
        [HttpGet]
        public ActionResult<List<Train>> Get()
        {
            return trainService.Get();
           
        }

        // GET api/<TrainController>/5
        [HttpGet("{id}")]
        public ActionResult<Train> Get(string id)
        {
            var train = trainService.Get(id);
            
            if (train == null)
            {
                return NotFound($"Train with id = {id} not found "); ;
            }

            return train;
        }

        // POST api/<TrainController>
        [HttpPost]
        public ActionResult<Train> Post([FromBody] Train train)
        {
            trainService.Create(train);

            return CreatedAtAction(nameof(Get), new { id = train.Id }, train);
        }

        // PUT api/<TrainController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Train train)
        {
            var trainToUpdate = trainService.Get(id);

            if (trainToUpdate == null)
            {
                return NotFound($"Train with id = {id} not found ");
            }

            trainService.Update(id, train);

            return NoContent();
        }

        // DELETE api/<TrainController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var trainToDelete = trainService.Get(id);

            if (trainToDelete == null)
            {
                return NotFound($"Train with id = {id} not found ");
            }

            var result = trainService.Delete(trainToDelete.Id);

            return Ok(result);
        }

        //update train status
        [HttpPatch("updateStatus/{id}")]
        public ActionResult UpdateStatus(string id)
        {
            var trainToUpdate = trainService.Get(id);

            if (trainToUpdate == null)
            {
                return NotFound($"Train with id = {id} not found ");
            }

            var result = trainService.UpdateStatus(id);

            return Ok(result);
        }

        [HttpPost("search")]
        public ActionResult<SearchResponse> GetAvailableTrains([FromBody] SearchRequest searchRequest)
        {
            var results = trainService.GetAvailableTrains(searchRequest);

            return results;
        }
    }
}
