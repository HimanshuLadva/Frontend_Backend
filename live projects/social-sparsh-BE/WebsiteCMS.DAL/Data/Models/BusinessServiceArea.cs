using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class BusinessServiceArea
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int BusinessInfoId { get; set; }
        public BusinessInfo BusinessInfo { get; set; }
    }
}
