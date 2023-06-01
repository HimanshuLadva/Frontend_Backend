namespace WebsiteCMS.DAL.Data.Models
{
    public class WCMSTemplates
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string StoredPathURL { get; set; }
        public string CoverImageURL { get; set; }

        public virtual ICollection<WCMSTemplatePages>? TemplatePages { get; set; }
        public virtual ICollection<WCMSUserTemplates>? UserTemplates { get; set; }
        public virtual ICollection<WCMSFieldsMasterChild>? TemplateFieldsMasterChild { get; set; }
    }
}