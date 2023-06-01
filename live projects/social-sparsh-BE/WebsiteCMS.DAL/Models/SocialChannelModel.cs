using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Models
{
    public class SocialChannelModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MasterType { get; set; }
        public string FieldType { get; set; }
        public List<SocialPlatformsModel> SocialPlatforms { get; set; }
    }
}
