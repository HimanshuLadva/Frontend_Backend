using CRMBackend.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace CRMBackend.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public int ContactCount { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        public bool IsActive { get; set; }
        public int ClientId { get; set; }
    }
}
