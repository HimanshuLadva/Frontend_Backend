namespace CRMBackend.Data.Models
{
    public class ContactGroups
    {
        public int ContactId { get; set; }
        public Contacts Contact { get; set; }
        public int GroupId { get; set; }
        public Groups Group { get; set; }
    }
}
