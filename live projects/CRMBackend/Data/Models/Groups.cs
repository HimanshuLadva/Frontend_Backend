using CRMBackend.Models;

namespace CRMBackend.Data.Models
{
    public class Groups
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        //public string ApplicationUserId { get; set; }
        //public ApplicationUser? ApplicationUser { get; set; }
        public int ClientId { get; set; }
        public Clients? Client { get; set; }
        public ICollection<ContactGroups>? ContactGroup { get; set; }
        public ICollection<Categories>? Categories { get; set; }
    }
}
