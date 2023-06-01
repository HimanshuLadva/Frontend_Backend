namespace WebsiteCMS.DAL.Data.Models
{
    public class SCRMBase
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
