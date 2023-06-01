using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class BOTImageOrFile
    {
        public int Id { get; set; }
        public string? FrontendId { get; set; }
        public string? ImageOrFilePath { get; set; }
    }
}
