using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class SocialPlateformWisePosts
    {
        public int Id { get; set; }

        [ForeignKey("SociaMediaPost")]
        public int PostId { get; set; }

        [ForeignKey("FacebookPagesTokens")]
        public int? PageId { get; set; }

        [ForeignKey("SocialPlatforms")]
        public int Plateformid { get; set; }

        public virtual SociaMediaPost? SociaMediaPost { get; set; }
        public virtual FacebookPagesTokens? FacebookPagesTokens { get; set; }
        public virtual SocialPlatforms? SocialPlatforms { get; set; }

    }
}
