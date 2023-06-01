using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Models
{
    public class WCMSFieldsMasterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MasterType { get; set; }
        public string FieldType { get; set; }
        public string Value { get; set; }
    }
    public class Set
    {
        public int SetId { get; set; }
        public List<WCMSFieldsMasterModel> SetValues { get; set; } = new List<WCMSFieldsMasterModel>();
    }
}
