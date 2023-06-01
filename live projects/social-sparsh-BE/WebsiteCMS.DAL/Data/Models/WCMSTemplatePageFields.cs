using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class WCMSTemplatePageFields
    {
        public int Id { get; set; }
        public int TemplatePageId { get; set; }
        public int FieldsMasterId { get; set; }

        public virtual WCMSTemplatePages? TemplatePage { get; set; }
        public virtual WCMSFieldsMaster? FieldsMaster { get; set; }
        public virtual ICollection<WCMSUserTemplateDetails>? UserTemplateDetails { get; set; }
        public virtual ICollection<WCMSUserTemplateDetailsChilds>? UserTemplateDetailsChilds { get; set; }
    }
}
