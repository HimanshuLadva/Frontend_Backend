using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class WCMSTemplatePageType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<WCMSTemplatePages>? TemplatePage { get; set; }
    }
}
