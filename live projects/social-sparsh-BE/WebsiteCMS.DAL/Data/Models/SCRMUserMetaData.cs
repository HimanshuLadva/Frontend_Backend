using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.DAL.Data.Models
{
    public class SCRMUserMetaData
    {
        public int Id { get; set; }

        [ForeignKey("IdentityUser")] 
        public string ApplicationUserId { get; set; }
        //public SCRMUser User { get; set; }
        public int TemplateFieldId { get; set; }
        public SCRMTemplateField TemplateField { get; set; }
        public string? Value { get; set; }
    }
}
