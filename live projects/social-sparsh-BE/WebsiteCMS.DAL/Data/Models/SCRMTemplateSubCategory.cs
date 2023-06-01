using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class SCRMTemplateSubCategory
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public SCRMTemplate Template { get; set; }
        public int SubCategoryId { get; set; }
        public SCRMSubCategory SubCategory { get; set; }
    }
}
