using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class BusinessPhoneNos
    {
        public int Id { get; set; }
        //public string? UserPhoneNoId { get; set; }
        public int ContactInfoId { get; set; }
        public BusinessContactInfo ContactInfo { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
