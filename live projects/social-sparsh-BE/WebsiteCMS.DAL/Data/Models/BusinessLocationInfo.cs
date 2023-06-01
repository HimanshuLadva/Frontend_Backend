using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class BusinessLocationInfo
    {
        public int Id { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? PinCode { get; set; }

        public int BusinessInfoId { get; set; }
        public BusinessInfo BusinessInfo { get; set; }

        public string? StreetAddressLine1 { get; set; }
        public string? StreetAddressLine2 { get; set; }
        public string? StreetAddressLine3 { get; set; }


    }
}
