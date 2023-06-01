using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class WCMSUserTemplateDetails
    {
        public int Id { get; set; }
        public int UserTemplateId { get; set; }
        public int TemplatePageFieldId { get; set; }
        public string? Value { get; set; }
        public int HasChilds { get; set; }

        public virtual WCMSUserTemplates? UserTemplate { get; set; }
        public virtual WCMSTemplatePageFields? TemplatePageField { get; set; }
        public virtual ICollection<WCMSUserTemplateDetailsChilds>? UserTemplateDetailsChilds { get; set; }
    }
}
