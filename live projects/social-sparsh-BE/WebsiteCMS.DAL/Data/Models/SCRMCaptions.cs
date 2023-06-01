using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class SCRMCaptions
    {
        public int Id { get; set; }

        public int? SCRMCategoryID { get; set; }
        public virtual SCRMCategory? SCRMCategory { get; set; }

        public int? SCRMSubCategoryId { get; set; }
        public virtual SCRMSubCategory? SCRMSubCategory { get; set; }
        public string Caption { get; set; }
    }
}
