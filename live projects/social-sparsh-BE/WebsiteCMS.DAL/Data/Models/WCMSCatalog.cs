using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.DAL.Data.Models
{
    public class WCMSCatalog
    {
        public long Id { get; set; }
        public string ItemId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Availability { get; set; }

        public string Condition { get; set; }

        public string Price { get; set; }

        public string Link { get; set; }

        public string ImageLink { get; set; }

        public string Brand { get; set; }

        public string? GoogleProductCategory { get; set; }

        public string? FBProductCategory { get; set; }

        public string? QuantityToSell { get; set; }

        public string? SellPrice { get; set; }

        public string? SellDuration { get; set; }

        public long? ItemGroupId { get; set; }

        public string? Gender { get; set; }

        public string? Colour { get; set; }

        public string? Size { get; set; }

        public string? Age { get; set; }

        public string? Material { get; set; }

        public string? Pattern { get; set; }

        public string? Shipping { get; set; }

        public string? ShippingWeight { get; set; }
    }
}
