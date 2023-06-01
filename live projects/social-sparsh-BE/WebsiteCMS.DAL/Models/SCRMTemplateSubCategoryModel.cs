using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Models
{
    public class SCRMTemplateSubCategoryModel
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string? Template { get; set; }
        public int SubCategoryId { get; set; }
        public string? SubCategory { get; set; }
        public List<SCRMCaptionsModel>? Captions { get; set; }

    }
}
