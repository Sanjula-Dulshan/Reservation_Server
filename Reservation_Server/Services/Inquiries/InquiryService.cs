using MongoDB.Driver;
using Reservation_Server.Database;
using Reservation_Server.Models.Inquiries;

namespace Reservation_Server.Services.Inquiries
{
    public class InquiryService : IInquiryService
    {
        private readonly IMongoCollection<Inquiry> _inquiries;  

        // Constructor for InquiryService: Initializes database and inquiry collection.
        public InquiryService(IDatabaseSettings settings, IMongoClient mongoClient)
        {

            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _inquiries = database.GetCollection<Inquiry>(settings.InquiriesCollectionName);

        }

        // Creates a new inquiry
        public Inquiry Create(Inquiry inquiry)
        {
            _inquiries.InsertOne(inquiry);
            return inquiry;
        }

        // Deletes the inquiry with the specified ID
        public void Delete(string id)
        {
            _inquiries.DeleteOne(inquiry => inquiry.Id == id);
        }

        // Retrieves a list of all inquiries
        public List<Inquiry> Get()
        {
            return _inquiries.Find(inquiry => true).ToList();
        }

        // Retrieves an inquiry given ID
        public Inquiry Get(string id)
        {
            return _inquiries.Find(inquiry => inquiry.Id == id).FirstOrDefault();
        }

        // Updates the inquiry with the specified ID
        public void Update(string id, Inquiry inquiry)
        {
            _inquiries.ReplaceOne(inquiry => inquiry.Id == id, inquiry);
        }

        // Updates the status of the inquiry with the specified ID
        public void UpdateStatus(string id)
        {
            Inquiry inquiry = _inquiries.Find(inquiry => inquiry.Id == id).FirstOrDefault();
            inquiry.Status = "confirm";
            _inquiries.UpdateOne(inquiry => inquiry.Id == id, Builders<Inquiry>.Update.Set("Status", inquiry.Status));
        }
    }
}
