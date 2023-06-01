using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class BusinessCategory
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public ICollection<BusinessInfoCategories>? BusinessCategoryList { get; set; }


    }
}
