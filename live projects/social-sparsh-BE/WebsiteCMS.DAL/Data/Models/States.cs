namespace WebsiteCMS.DAL.Data.Models
{
    public class States
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CountryId { get; set; }
        public Countries Country { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
