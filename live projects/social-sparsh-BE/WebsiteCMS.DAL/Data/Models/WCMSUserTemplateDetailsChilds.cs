using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class WCMSUserTemplateDetailsChilds
    {
        public int Id { get; set; }
        public int UserTemplateDetailsId { get; set; }
        public int Group { get; set; }
        public int TemplatePageFieldsId { get; set; }
        public string? Value { get; set; }

        public virtual WCMSUserTemplateDetails? UserTemplateDetails { get; set; }
        public virtual WCMSTemplatePageFields? TemplatePageFields { get; set; }
    }
}
