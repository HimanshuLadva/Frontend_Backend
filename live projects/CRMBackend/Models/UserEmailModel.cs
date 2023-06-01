using CRMBackend.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace CRMBackend.Models
{
    public class UserEmailModel
    {
        public int Id { get; set; }
        [Required]
        public string? Subject { get; set; }
        [Required]
        public string? Body { get; set; }
        [Required]
        public string? RecipientCollection { get; set; }
        public ICollection<RecipientsEmailModel>? Recipients { get; set; }
        public int ClientId { get; set; }
    }
}
