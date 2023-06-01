using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Models
{
    public class SocialPlateformWisePostsModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int? PageId { get; set; }
        public int Plateformid { get; set; }
        public string PlateformType { get; set; }

        public virtual SociaMediaPostModel? SociaMediaPost { get; set; }
        public virtual AllFBPageAndLinkedInstagramAccountModel? FacebookPagesTokens { get; set; }
        public virtual SocialPlatformsModel? SocialPlatforms { get; set; }
    }
}
