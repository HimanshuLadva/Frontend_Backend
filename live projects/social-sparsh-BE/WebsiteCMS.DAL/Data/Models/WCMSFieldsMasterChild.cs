using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class WCMSFieldsMasterChild
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Value { get; set; }
        public int TemplateId { get; set; }
        public int FieldsMasterId { get; set; }
        public int MasterTypeId { get; set; }
        public int Group { get; set; }

        public virtual WCMSFieldsMaster? FieldsMaster { get; set; }
        public virtual WCMSMasterType? MasterType { get; set; }
        public virtual WCMSTemplates? Template { get; set; }
    }
}
