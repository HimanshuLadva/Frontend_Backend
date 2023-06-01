namespace CRMBackend.Data.Models
{
    public class Events
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime EventDate { get; set; }
        public bool IsActive { get; set; } = true;
        public int ClientId { get; set; }
        public Clients? Client { get; set; }
        public ICollection<ContactEvents>? ContactEvent { get; set; }
    }
}
