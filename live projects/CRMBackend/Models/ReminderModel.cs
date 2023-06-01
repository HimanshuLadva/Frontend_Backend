using CRMBackend.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace CRMBackend.Models
{
    public class ReminderModel
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        public int ClientId { get; set; }
    }
}
