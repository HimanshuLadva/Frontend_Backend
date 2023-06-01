using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Models
{
    public class WCMSGlobleSettingsViewModel
    {
        public List<WCMSTemplatesModel> Templates { get; set; }
        public WCMSFieldsMasterModel GATag { get; set; }
        public WCMSFieldsMasterModel FacebookPixel { get; set; }
        public SocialChannelModel SocialChannels { get; set; }
    }
}
