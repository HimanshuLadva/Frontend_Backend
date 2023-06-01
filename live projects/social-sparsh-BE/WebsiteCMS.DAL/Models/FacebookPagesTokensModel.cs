using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Models
{
    public class FacebookPagesTokensModel
    {
        public List<FacebookPageModel> data { get; set; }
    }
    /* picture, cover, name, username, about, bio, birthday, category_list, category, contact_address, followers_count, likes, phone, whatsapp_number, connected_instagram_account, single_line_address*/
    public class FacebookPageDetailModel
    {
        public FacebookPagePictureModel? picture { get; set; }
        public FacebookPageCoverModel? cover { get; set; }
        public string? id { get; set; }
        public string? name { get; set; }
        public string? about { get; set; }
        public string? bio { get; set; }
        public string? birthday { get; set; }
        public string? category { get; set; }
        public ICollection<SCRMUserCategoryListModel>? category_list { get; set; }
        public string? contact_address { get; set; }
        public int followers_count { get; set; } = 0;
        public int likes { get; set; } = 0;
        public string? phone { get; set; }
        public string? whatsapp_number { get; set; }
        public string? connected_instagram_account { get; set; }
        public string? single_line_address { get; set; }
    }

    public class FacebookPagePictureModel
    {
        public FacebookPagePictureDetailModel data { get; set; }
    }
    public class FacebookPagePictureDetailModel
    {
        public int? height { get; set; }
        public bool? is_silhouette { get; set; }
        public string? url { get; set; }
        public int? width { get; set; }
    }
    public class FacebookPageCoverModel
    {
        public string? cover_id { get; set; }
        public int? offset_x { get; set; }
        public int? offset_y { get; set; }
        public string? source { get; set; }
        public string? id { get; set; }
    }


    public class SCRMUserCategoryListModel
    {
        public string id { get; set; }
        public string name { get; set; }

    }
    public class FacebookPageModel
    {
        public int? Id { get; set; }
        public string UserId { get; set; }
        public long FacebookId { get; set; }
        public string? name { get; set; }
        public string? access_token { get; set; }
        public string? ProfilePicture { get; set; }
    }

    public class AllFBPageAndLinkedInstagramAccountModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public long FacebookId { get; set; }
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
        public string? ContactAddress { get; set; }
        public int? FollowersCount { get; set; } = 0;
        public int? Likes { get; set; } = 0;
        public string? Phone { get; set; }
        public string? WhatsappNumber { get; set; }
        public string? ConnectedInstagramAccount { get; set; }
        public string? SingleLineAddress { get; set; }
    }
}
