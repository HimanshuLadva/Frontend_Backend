using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteCMS.DAL.Data.Models
{
    public class SCRMTemplate : SCRMBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public int CategoryId { get; set; }
        //public SCRMCategory Category { get; set; }
        public ICollection<SCRMTemplateTag> Tags { get; set; }
        public string? TemplateImageURL { get; set; }
        public string PublicTemplateImageURL { get; set; }
        public string PremiumTemplateImageURL { get; set; }
        public string PublicTemplatePreviewImageURL { get; set; }
        public string PremiumTemplatePreviewImageURL { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsFree { get; set; } = true;
        public int LanguageId { get; set; }
        public SCRMLanguage? Language { get; set; }
        public int ColorId { get; set; }
        public SCRMColor? Color { get; set; }
        public ICollection<SCRMTemplateSubCategory> SubCategory { get; set; }
        public ICollection<SCRMTemplateCategory> Category { get; set; }
    }
}
