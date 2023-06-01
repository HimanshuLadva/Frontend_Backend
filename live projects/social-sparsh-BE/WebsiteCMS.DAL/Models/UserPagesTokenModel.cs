using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Models
{
    public class UserPagesTokenModel
    {
        public string? UserId { get; set; }
        public string UserName { get; set; }
        public long FacebookId { get; set; }
        public List<PageList> Lists { get; set; }
    }
    public class PageList
    {
        public long PageId { get; set; }
        public string PageName { get; set; }
        public string Access_token { get; set; }
    }
}
