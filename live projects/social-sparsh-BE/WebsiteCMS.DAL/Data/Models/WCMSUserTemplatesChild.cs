using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class WCMSUserTemplatesChild
    {
        public int Id { get; set; }
        public int UserTemplateId { get; set; }
        public int PlatformId { get; set; }
        public string Value { get; set; }
        public virtual WCMSUserTemplates? UserTemplate { get; set; }
        public virtual SocialPlatforms? Platform { get; set; }
    }
}
