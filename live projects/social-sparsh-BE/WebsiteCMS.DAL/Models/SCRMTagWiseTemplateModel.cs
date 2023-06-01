using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Models
{
    public class SCRMTagWiseTemplateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SCRMTemplateWithLayoutModel> TemplateAndLayout { get; set; }
    }

    public class SCRMCategroyWiseTemplateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SCRMTemplateWithLayoutModel> TemplateAndLayout { get; set; }
    }

    public class SCRMMultipleCategoryWiseTemplateModel
    {
        public int category_id { get; set; }
        public string category_name { get; set; }
        public List<SCRMTemplateWithLayoutModel> TemplateList { get; set; }
    }
}
