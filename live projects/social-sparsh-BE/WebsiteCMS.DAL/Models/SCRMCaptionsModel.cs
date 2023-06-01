using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Models
{
    public class SCRMCaptionsModel
    {
        public int Id { get; set; }
        public int? SCRMCategoryID { get; set; }
        public int? SCRMSubCategoryId { get; set; }
        public string Caption { get; set; }
    }
}
