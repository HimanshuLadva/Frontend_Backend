using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class BOTWhatsAppTemplates
    {
        public long Id { get; set; }
        public string WhatsAppTemplateId { get; set; }
        public string WhatsAppTemplateName { get; set; }
        public string Language { get; set; }
        public long QuestionId { get; set; }
        public BOTQuestion Question { get; set; }
    }
}
