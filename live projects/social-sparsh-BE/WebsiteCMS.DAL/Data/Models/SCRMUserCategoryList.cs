using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class SCRMUserCategoryList
    {
        public int Id { get; set; }
        public string? CategoryId { get; set; }
        public string? Name { get; set; }

        public int FBTokenId { get; set; }
        public FacebookPagesTokens FBToken { get; set; }
    }
}
