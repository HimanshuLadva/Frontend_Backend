using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Models
{
    public class WCMSTemplateFieldsMasterModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Value { get; set; }
        public int TemplateId { get; set; }
        public int FieldsMasterId { get; set; }
        public string MasterType { get; set; }
        public int Group { get; set; }

        public WCMSTemplateFieldsMasterModel? fieldsMaster { get; set; }
        public WCMSTemplatesModel? Template { get; set; }
    }
}
