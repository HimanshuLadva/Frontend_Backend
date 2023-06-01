using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class SCRMFontFamily : SCRMBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Path { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
