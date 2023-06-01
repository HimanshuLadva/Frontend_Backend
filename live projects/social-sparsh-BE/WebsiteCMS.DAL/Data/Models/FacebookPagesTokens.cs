using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class FacebookPagesTokens
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public long SocialId { get; set; }
        public long PageId { get; set; }
        public string? PageName { get; set; }
        public string? Access_token { get; set; }

        public string? PictureUrl { get; set; }

        public string? Cover { get; set; }
        public string? Name { get; set; }
        //public string UserName { get; set; }
        public string? About { get; set; }
        public string? Bio { get; set; }
        public string? Birthday { get; set; }
        public string? Category { get; set; }

        public ICollection<SCRMUserCategoryList>? CategoryList { get; set; }
        public string? ContactAddress { get; set; }
        public int? FollowersCount { get; set; } = 0;
        public int? Likes { get; set; } = 0;
        public string? Phone { get; set; }
        public string? WhatsappNumber { get; set; }
        public string? ConnectedInstagramAccount { get; set; }
        public string? SingleLineAddress { get; set; }
        public int SocialPlatformsId { get; set; }

        public virtual SocialPlatforms? SocialPlatforms { get; set; }
        public virtual ICollection<SocialPlateformWisePosts>? SocialPlateformWisePosts { get; set; }

        /* picture, cover, name, username, about, bio, birthday, category_list, category, contact_address, followers_count, likes, phone, whatsapp_number, connected_instagram_account, single_line_address*/
    }
}
