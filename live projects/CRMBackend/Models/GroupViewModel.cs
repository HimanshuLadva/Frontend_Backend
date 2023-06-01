using CRMBackend.Data.Models;

namespace CRMBackend.Models
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int ClientId { get; set; }
    }
}
