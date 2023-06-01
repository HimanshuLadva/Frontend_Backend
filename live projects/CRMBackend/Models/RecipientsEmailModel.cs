using CRMBackend.Data.Models;

namespace CRMBackend.Models
{
    public class RecipientsEmailModel
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public int UserEmailId { get; set; }
    }
}
