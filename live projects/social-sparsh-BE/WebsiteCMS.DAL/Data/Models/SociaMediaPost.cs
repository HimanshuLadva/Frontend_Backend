using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class SociaMediaPost
    {
        public int Id { get; set; }
        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public string PostImage { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime PostDate { get; set; }
        public string Caption { get; set; }

        public virtual ICollection<SocialPlateformWisePosts>? SocialPlateformWisePosts { get; set; }

    }
}
