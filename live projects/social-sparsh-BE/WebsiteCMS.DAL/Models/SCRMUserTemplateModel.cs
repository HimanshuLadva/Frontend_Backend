using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Models
{
    public class SCRMUserTemplateModel
    {
        public string ApplicationUserId { get; set; }
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateImageURL { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<SCRMTemplateTagModel> Tags { get; set; }
        public ICollection<SCRMTemplateFieldTextModel> TextFields { get; set; }
        public ICollection<SCRMTemplateFieldImageModel> ImageFields { get; set; }
        public bool IsFree { get; set; } = false;
    }
}
