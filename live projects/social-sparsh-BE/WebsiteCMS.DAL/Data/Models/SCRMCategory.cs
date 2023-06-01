namespace WebsiteCMS.DAL.Data.Models
{
    public class SCRMCategory : SCRMBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string? CategoryImage { get; set; }
        public ICollection<SCRMSubCategory> SubCategory { get; set; }
        public ICollection<SCRMCaptions> Captions { get; set; }
    }
}
