using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class WCMSTemplatePages
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string DisplayPageName { get; set; }
        public string PagePath { get; set; }
        public int TemplatePageTypeId { get; set; }

        public virtual WCMSTemplates? Template { get; set; }
        public virtual WCMSTemplatePageType? TemplatePageType { get; set; }
        public virtual ICollection<WCMSTemplatePageFields>? TemplatePageFields { get; set; }
    }
}
