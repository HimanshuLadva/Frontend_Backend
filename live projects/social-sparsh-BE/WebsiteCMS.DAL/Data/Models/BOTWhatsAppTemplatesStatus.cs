using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class BOTWhatsAppTemplatesStatus
    {
        public long Id { get; set; }
        public string WhatsAppTemplateId { get; set; }
        public string Status { get; set; }
    }
}
