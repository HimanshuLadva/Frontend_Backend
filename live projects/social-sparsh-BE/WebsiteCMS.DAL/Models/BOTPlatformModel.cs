using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Models
{
    public class BOTPlatformModel
    {
        public long? Id { get; set; }
        public long ChatBotId { get; set; }
        public string Platform { get; set; }
    }
}
