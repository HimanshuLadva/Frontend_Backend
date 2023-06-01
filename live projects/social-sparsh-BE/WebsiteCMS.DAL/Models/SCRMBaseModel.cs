namespace WebsiteCMS.DAL.Models
{
    public class SCRMBaseModel
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
