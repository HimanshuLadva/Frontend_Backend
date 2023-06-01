using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Models
{
    public class AWSS3ObjectModel
    {
        public string? Name { get; set; }
        public MemoryStream? InputStream { get; set; }
        public string? BucketName { get; set; }
    }
}
