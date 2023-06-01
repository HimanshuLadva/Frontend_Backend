using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Models
{
    public class SCRMTemplateLayoutModel
    {
        public int Id { get; set; }
        public string? TemplateImageURL { get; set; }
        public ICollection<SCRMTemplateFieldTextModel> TextFields { get; set; }
        public ICollection<SCRMTemplateFieldImageModel> ImageFields { get; set; }
    }
}
