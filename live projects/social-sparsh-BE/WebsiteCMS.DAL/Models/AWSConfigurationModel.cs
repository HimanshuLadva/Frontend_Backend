using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Models
{
    public class AWSConfigurationModel
    {
        public string? AWSAccessKey { get; set; }
        public string? AWSSecretKey { get; set; }
        public string? BucketName { get; set; }
        public string? AWSImageUrl { get; set; }
    }
}
