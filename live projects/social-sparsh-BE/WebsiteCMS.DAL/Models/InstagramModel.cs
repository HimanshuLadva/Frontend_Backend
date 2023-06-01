using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Models
{
    public class InstagramModel
    {
        public List<InstagramPostModel> data { get; set; }
    }

    public class InstagramPostModel
    {
        public string id { get; set; }
    }
    public class InstagramAccountLinkedWithFbPageModel
    {
        public string name { get; set; }
        public InstaId instagram_business_account { get; set; }
    }

    public class InstaId
    {
        public string id { get; set; }
    }
    public class InstagramPostDetailModel
    {
        public string id { get; set; }
        public string media_type { get; set; }
        public string permalink { get; set; }
        public string media_url { get; set; }
        public string username { get; set; }
        public string caption { get; set; }
        public string timestamp { get; set; }

    }

    public class InstagramAccountDetailModel
    {
        public int followers_count { get; set; }
        public int follows_count { get; set; }
        public int media_count { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string id { get; set; }
    }
}
