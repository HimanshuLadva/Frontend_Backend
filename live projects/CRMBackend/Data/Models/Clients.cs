using CRMBackend.Models;

namespace CRMBackend.Data.Models
{
    public class Clients
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public ICollection<Groups>? ManageGroups { get; set; }
        public ICollection<Events>? ManageEvents { get; set; }
        public ICollection<UserSMSs>? UserSMS { get; set; }
        public ICollection<UserEmails>? UserEmail { get; set; }
        public ICollection<Contacts>? Contacts { get; set; }
        public ICollection<Reminders>? Reminder { get; set; }
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        //public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
