namespace CRMBackend.Data.Models
{
    public class ContactEvents
    {
        public int ContactsId { get; set; }
        public Contacts Contacts { get; set; }
        public int EventsId { get; set; }
        public Events Events { get; set; }

    }
}
