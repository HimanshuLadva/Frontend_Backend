using CRMBackend.Data.Models;

namespace CRMBackend.Models
{
    public class RecipientsPhoneNoModel
    {
        public int Id { get; set; }
        public string? PhoneNo { get; set; }
        public int UserSMSId { get; set; }
    }
}
