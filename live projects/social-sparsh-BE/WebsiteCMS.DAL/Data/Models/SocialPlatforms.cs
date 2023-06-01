using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class SocialPlatforms
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FacebookPagesTokens> FacebookPagesTokens { get; set; }
        public virtual ICollection<SocialPlateformWisePosts>? SocialPlateformWisePosts { get; set; }
    }
}
