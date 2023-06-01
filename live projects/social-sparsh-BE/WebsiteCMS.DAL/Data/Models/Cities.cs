using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteCMS.DAL.Data.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatesId { get; set; }
        public virtual States States { get; set; }

        public int CountriesId { get; set; }
        public Countries Countries { get; set; }
    }
}
