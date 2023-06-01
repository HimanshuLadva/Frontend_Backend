using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class BOTWebHookResponse
    {
        public long Id { get; set; }
        public string? BussinessId { get; set; }
        public string? PhoneNumberId { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Message { get; set; }
        public string? ResponseBody { get; set; }
        public string? TimeStamp { get; set; }
        public long BOTVisitorsId { get; set; }
        public BOTVisitor BOTVisitors { get; set; }

    }
}
