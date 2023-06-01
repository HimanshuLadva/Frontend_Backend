using Microsoft.AspNetCore.Http;

namespace WebsiteCMS.DAL.Models
{
    public class WCMSTemplatesModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? StoredPathURL { get; set; }
        public string? CoverImageURL { get; set; }
        public IFormFile? TemplateImage { get; set; }
        public IFormFile? TemplateZip { get; set; }

        public List<WCMSUserTemplatesModel> UserTemplateInfos { get; set; } = new List<WCMSUserTemplatesModel>();
        public virtual List<WCMSTemplateFieldsMasterModel>? TemplateFieldsMaster { get; set; }
    }
}