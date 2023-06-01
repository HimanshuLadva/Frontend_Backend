using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Models
{
    public class WCMSGlobleSettingsModel
    {
        public int TemplateId { get; set; }
        public string GATagId { get; set; }
        public string facebookPixelId { get; set; }
        public List<SocialChannel> SocialChannels { get; set; }
        public int ColorGroupId { get; set; }
        public int FontGroupId { get; set; }
    }


    public class SocialChannel
    {
        public int PlateformId { get; set; }
        public string Value { get; set; }
    }
}
