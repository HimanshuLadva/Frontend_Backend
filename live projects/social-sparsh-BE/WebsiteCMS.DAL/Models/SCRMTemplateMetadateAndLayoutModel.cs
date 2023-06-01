using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Models
{
    public class SCRMTemplateMetadateAndLayoutModel
    {
        public int TemplateId { get; set; }
        public SCRMTemplateModel TemplateMetadata { get; set; }
        public SCRMTemplateLayoutModel TemplateLayout { get; set; }
    }
}
