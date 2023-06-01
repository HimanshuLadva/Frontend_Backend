using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Models
{
    public class BOTImageOrFileModel
    {

        public string? FrontendId { get; set; }
        public IFormFile? ImageOrFilePath { get; set; }
    }
}
