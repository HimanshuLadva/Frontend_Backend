using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class BusinessContactInfo
    {
        public int Id { get; set; }
        public ICollection<BusinessPhoneNos>? UserPhoneNos { get; set; }

        public string? Email { get; set; }
        public string? Website { get; set; }
        public int BusinessInfoId { get; set; }
        public BusinessInfo BusinessInfo { get; set; }
    }
}
