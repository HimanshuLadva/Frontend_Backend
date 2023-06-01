using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Models
{
    public class SCRMTemplateWithLayoutModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string? Category { get; set; }
        public string? TemplateImageURL { get; set; }
        public bool IsActive { get; set; }
        public bool IsFree { get; set; }
        public ICollection<SCRMTemplateFieldTextModel> TextFields { get; set; }
        public ICollection<SCRMTemplateFieldImageModel> ImageFields { get; set; }
    }
}
