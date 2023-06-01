using CRMBackend.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace CRMBackend.Models
{
    public class UserSMSModel
    {
        public int Id { get; set; }
        [Required]
        public string? Message { get; set; }
        [Required]
        public string? RecipientCollection { get; set; }
        public ICollection<RecipientsPhoneNoModel>? Recipients { get; set; }
        public int ClientId { get; set; }
    }
}
