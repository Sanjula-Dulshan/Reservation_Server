using Reservation_Server.Models.Inquiries;

namespace Reservation_Server.Services.Inquiries
{
    public interface IInquiryService
    {

        List<Inquiry> Get();
        Inquiry Get(string id);
        Inquiry Create(Inquiry inquiry);
        void Update(string id, Inquiry inquiry);
        void Delete(string id);
        void UpdateStatus(string id);
    }
}
