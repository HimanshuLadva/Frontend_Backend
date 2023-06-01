namespace WebsiteCMS.DAL.Data.Models
{
    public class SCRMSubCategory : SCRMBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public bool IsActive { get; set; } = true;
        public int CategoryId { get; set; }
        public string? SubCategoryImage { get; set; }
        public SCRMCategory Category { get; set; }
        public ICollection<SCRMCaptions> Captions { get; set; }
    }
}
