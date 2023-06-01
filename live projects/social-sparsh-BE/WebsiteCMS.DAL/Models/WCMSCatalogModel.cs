using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Models
{
    public enum Availability_Item
    {
        InStock,
        AvailableForOrder,
        PreOrder,
        OutOfStock,
        DisContinued
    }

    public enum Condition_of_Item
    {
        New,
        Used,
        Refurbished
    }

    public enum Targeted_Gender
    {
        Male,
        Female,
        Unisex
    }

    public enum Age_Group
    {
        Adult,
        AllAges,
        Infant,
        Kids,
        NewBorn,
        Teen,
        Toddler
    }

    [Index(nameof(ItemId), IsUnique = true)]
    public class WCMSCatalogModel
    {
        public long Id { get; set; }

        [Required, Column("Item_SKU"), MaxLength(100)]
        public string ItemId { get; set; }

        [Required, Column("Item_Title"), MaxLength(150)]
        public string Title { get; set; }

        [Required, Column("About_Item"), MaxLength(9999)]
        public string Description { get; set; }

        [Required, Column("Item_Availability")]
        public Availability_Item Availability { get { return Availability; } set { if (Enum.IsDefined(typeof(Availability_Item), value)) Availability = value; else throw new Exception("Availabiliry_Item must be InStock, AvailableForOrder, PreOrder, OutOfStock Or DisContinued"); } }

        [Required, Column("Item_Condition")]
        public Condition_of_Item Condition { get { return Condition; } set { if (Enum.IsDefined(typeof(Condition_of_Item), value)) Condition = value; else throw new Exception("Condition of Item must be New, Used or Refurbished"); } }

        [DisplayFormat(DataFormatString = "00.00 USD", ApplyFormatInEditMode = true)]
        [Required, Column("Item_Price")]
        public string Price { get; set; }

        [Required, Column("From_Item_Sell")]
        public string Link { get; set; }

        [Required, Column("Item_Image")]
        public string ImageLink { get; set; }

        [Required, Column("Item_Brand"), MaxLength(100)]
        public string Brand { get; set; }

        [Column("Google_Product_Category")]
        public string? GoogleProductCategory { get; set; }

        [Column("FB_Product_Category")]
        public string? FBProductCategory { get; set; }

        [Column("Quantity_to_sell_on_Facebook"), Range(0, int.MaxValue)]
        public string? QuantityToSell { get; set; }

        [DisplayFormat(DataFormatString = "00.00 USD", ApplyFormatInEditMode = true)]
        [Column("Sale_Price")]
        public string? SellPrice { get; set; }

        [DisplayFormat(DataFormatString = "YYYY-MM-DDT23:59-00:00/YYYY-MM-DDT23:59-00:00", ApplyFormatInEditMode = true)]
        [Column("Sale_Price_Effective_Date")]
        public string? SellDuration { get { return SellDuration; } set { if (SellPrice != null) SellDuration = null; else SellDuration = value; } }

        [Column("Item_Group")]
        public long? ItemGroupId { get; set; }

        [Column("Targeted_Gender")]
        public Targeted_Gender Gender { get { return Gender; } set { if (Enum.IsDefined(typeof(Targeted_Gender), value)) Gender = value; else throw new Exception("Gender must be from Female, Male or Unisex"); } }

        [Column("Item_colour"), MaxLength(200)]
        public string? Colour { get { return Colour ?? ""; } set { Colour = value != null ? value!.Replace("#", "") : null; } }

        [Column("Item_Size"), MaxLength(200)]
        public string? Size { get; set; }

        [Column("Targeted_Age")]
        public Age_Group Age { get { return Age; } set { if (Enum.IsDefined(typeof(Age_Group), value)) Age = value; else throw new Exception("Targeted Age must be from Adult, AllAges, Infant, Kids, NewBorn, Teen, Toddler"); } }

        [Column("Item_Material"), MaxLength(200)]
        public string? Material { get; set; }

        [Column("Item_Print"), MaxLength(100)]
        public string? Pattern { get; set; }

        public string? Shipping_Country { get { return Shipping_Country; } set { Shipping_Country = value ?? ""; } }
        public string? Shipping_Region { get { return Shipping_Region; } set { Shipping_Region = value ?? ""; } }
        public string? Shipping_Service { get { return Shipping_Service; } set { Shipping_Service = value ?? ""; } }
        public string? Shipping_Price { get { return Shipping_Price; } set { Shipping_Price = value ?? ""; } }


        [Column("Shipping_Details")]
        public string? Shipping { get { return Shipping ?? null; } set { Shipping = $"{Shipping_Country}:{Shipping_Region}:{Shipping_Service}:{Shipping_Price}"; } }

        [Column("Shipping_Weight")]
        public string? ShippingWeight { get; set; }
    }
}
