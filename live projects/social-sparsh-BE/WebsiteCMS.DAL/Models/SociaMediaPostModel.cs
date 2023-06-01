using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Models
{
    public class SociaMediaPostModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string PostImage { get; set; }
        public DateTime PostDate { get; set; }
        public string Caption { get; set; }

        public List<SocialPlateformWisePostsModel>? SocialPlateformWisePosts { get; set; }
    }
}
