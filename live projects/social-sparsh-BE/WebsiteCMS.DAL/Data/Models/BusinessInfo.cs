using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Data.Models
{
    public class BusinessInfo
    {
        public int Id { get; set; }
        public string? BusinessName { get; set; }

        public ICollection<BusinessInfoCategories>? BusinessCategoryList { get; set; }
        public string? Description { get; set; }

        public string? TypeOfService { get; set; }
        public DateTime? OpeningDate { get; set; }

        public BusinessLocationInfo LocationInfo { get; set; }
        public BusinessContactInfo ContactInfo { get; set; }

        public ICollection<BusinessServiceArea>? businessServiceAreas { get; set; }

    }
}
