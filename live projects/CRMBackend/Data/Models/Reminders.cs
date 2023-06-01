namespace CRMBackend.Data.Models
{
    public class Reminders
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        //public int AdminWiseUsersId { get; set; }
        //public AdminWiseUsers AdminWiseUsers { get; set; }
        public int ClientId { get; set; }
        public Clients? Client { get; set; }

    }
}
