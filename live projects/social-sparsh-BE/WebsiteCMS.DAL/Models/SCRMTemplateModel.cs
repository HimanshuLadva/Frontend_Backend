using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.DAL.Models
{
    public class SCRMTemplateModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Template Name")]
        public string Name { get; set; }
        public ICollection<SCRMTemplateTagModel>? Tags { get; set; }
        public string TagCollection { get; set; }
        public string? TemplateImageURL { get; set; }
        public string? PublicTemplateImageURL { get; set; }
        public string? PremiumTemplateImageURL { get; set; }
        public string? PublicTemplatePreviewImageURL { get; set; }
        public string? PremiumTemplatePreviewImageURL { get; set; }

        public IFormFile? TemplateImage { get; set; }

        [Required(ErrorMessage = "Please Enter Template Status")]
        public bool IsActive { get; set; }
        public bool IsFree { get; set; }
        public int LanguageId { get; set; }
        public string? LanguageName { get; set; }
        public ICollection<SCRMTemplateSubCategoryModel>? SubCategories { get; set; }
        public string SubCategoryCollection { get; set; }
        public int ColorId { get; set; }
        public string? ColorName { get; set; }
        public ICollection<SCRMTemplateCategoryModel>? Categories { get; set; }
        public string CategoryCollection { get; set; }

    }

    public class SCRMTemplateUrls
    {
        public string? regularImageUrl { get; set; }
        public string? previewImageUrl { get; set; }
    }
}
