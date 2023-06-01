using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class WCMSFieldsMaster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MasterTypeId { get; set; }
        public string Key { get; set; }
        public int FieldtypeId { get; set; }
        public string? Syntax { get; set; }
        public int ParentId { get; set; }
        public string? Selector { get; set; }
        public string? NewSelector { get; set; }
        public bool IsOptional { get; set; }
        public bool? IsUserVisible { get; set; }


        public virtual WCMSFieldType? FieldType { get; set; }
        public virtual WCMSMasterType? MasterType { get; set; }
        public virtual ICollection<WCMSTemplatePageFields>? TemplatePageFields { get; set; }
        public virtual ICollection<WCMSFieldsMasterChild>? TemplateFieldsMaster { get; set; }
    }
}
