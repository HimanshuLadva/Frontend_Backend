using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.DAL.Data.Models
{
    public class WCMSUserTemplates
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        [ForeignKey("IdentityUser")]
        public string? ApplicationUserId { get; set; }
        public int IsPreview { get; set; }
        public int? ColorGroupId { get; set; }
        public int? FontGroupId { get; set; }
        public string? GATagId { get; set; }
        public string? FacebookPixelId { get; set; }
        public string? Domain { get; set; }

        public virtual WCMSTemplates? Template { get; set; }
        public virtual ICollection<WCMSUserTemplateDetails>? UserTemplateDetails { get; set; }
        public virtual ICollection<WCMSUserTemplatesChild>? UserTemplatesChild { get; set; }


    }
}
