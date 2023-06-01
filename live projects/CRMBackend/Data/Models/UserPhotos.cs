namespace CRMBackend.Data.Models
{
    public class UserPhotos
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public int ContactsId { get; set; }
        public Contacts? Contacts { get; set; }
    }
    public class UserNotes
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int ContactsId { get; set; }
        public Contacts? Contacts { get; set; }
    }
    public class UserDocuments
    {
        public int Id { get; set; }
        public string? DocumentUrl { get; set; }
        public int ContactsId { get; set; }
        public Contacts? Contacts { get; set; }
    }
}
