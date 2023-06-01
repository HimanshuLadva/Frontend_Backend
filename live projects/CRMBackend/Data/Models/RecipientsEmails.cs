namespace CRMBackend.Data.Models
{
    public class RecipientsEmails
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public int UserEmailId { get; set; }
        public UserEmails? UserEmail { get; set; }
    }
}
