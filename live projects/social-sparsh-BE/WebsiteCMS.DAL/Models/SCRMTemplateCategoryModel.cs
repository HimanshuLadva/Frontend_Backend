using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Models
{
    public class SCRMTemplateCategoryModel
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string? Template { get; set; }
        public int CategoryId { get; set; }
        public string? Category { get; set; }
        public ICollection<SCRMCaptionsModel>? Captions { get; set; }
    }
}
