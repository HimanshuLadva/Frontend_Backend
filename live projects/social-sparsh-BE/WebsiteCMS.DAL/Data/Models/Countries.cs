namespace WebsiteCMS.DAL.Data.Models
{
    public class Countries
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<States> States { get; set; }

    }
}
