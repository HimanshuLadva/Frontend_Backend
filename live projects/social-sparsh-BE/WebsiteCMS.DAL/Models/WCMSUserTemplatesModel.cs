namespace WebsiteCMS.DAL.Models
{
    public class WCMSUserTemplatesModel
    {
        public int Id { get; set; }

        public int TemplateId { get; set; }
        public string? ApplicationUserId { get; set; }
        public int IsPreview { get; set; }
    }
}