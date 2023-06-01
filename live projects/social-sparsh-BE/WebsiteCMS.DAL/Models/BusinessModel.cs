using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Models
{
    public class BusinessModel
    {
        public int Id { get; set; }
        public string? BusinessName { get; set; }
        public ICollection<BusinessInfoCategoriesModel>? BusinessCategoryList { get; set; }
        public string BusinessCategoryColleciton { get; set; }
        public string? Description { get; set; }
        public string? TypeOfService { get; set; }
        public DateTime? OpeningDate { get; set; }
        public ICollection<BusinessServiceAreaModel>? businessServiceAreas { get; set; }
        public string businessServiceAreaCollecitons { get; set; }
        public ICollection<BusinessPhoneNosModel>? UserPhoneNos { get; set; }
        public string UserPhoneCollection { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }

        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? PinCode { get; set; }
        public string? StreetAddressLine1 { get; set; }
        public string? StreetAddressLine2 { get; set; }
        public string? StreetAddressLine3 { get; set; }

    }
    public class BusinessInfoModel
    {
        public int Id { get; set; }
        public string? BusinessName { get; set; }
        public ICollection<BusinessInfoCategoriesModel>? BusinessCategoryList { get; set; }
        public string? Description { get; set; }
        public string? TypeOfService { get; set; }
        public DateTime? OpeningDate { get; set; }
        public BusinessLocationInfoModel LocationInfo { get; set; }
        public ICollection<BusinessServiceAreaModel>? businessServiceAreas { get; set; }
    }

    public class BusinessContactInfoModel
    {
        public int Id { get; set; }
        public ICollection<BusinessPhoneNosModel>? UserPhoneNos { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
    }
    public class BusinessInfoCategoriesModel
    {
        public int Id { get; set; }
        public int BusinessInfoId { get; set; }
        public BusinessInfoModel BusinessInfo { get; set; }
        public int BusinessCategoryId { get; set; }
        public BusinessCategoryModel BusinessCategory { get; set; }
    }

    public class BusinessServiceAreaModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BusinessInfoId { get; set; }
        public BusinessContactInfoModel BusinessInfo { get; set; }
    }

    public class BusinessLocationInfoModel
    {
        public int Id { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? PinCode { get; set; }
        public int BusinessInfoId { get; set; }
        public BusinessInfoModel BusinessInfo { get; set; }
        public string? StreetAddressLine1 { get; set; }
        public string? StreetAddressLine2 { get; set; }
        public string? StreetAddressLine3 { get; set; }
    }

    public class BusinessPhoneNosModel
    {
        public int Id { get; set; }
        public string? UserPhoneNoId { get; set; }
        public int ContactInfoId { get; set; }
        public BusinessContactInfoModel ContactInfo { get; set; }
        public string? PhoneNumber { get; set; }

    }

    public class BusinessCategoryModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Category Name")]
        public string Name { get; set; }
        public ICollection<BusinessInfoCategoriesModel>? BusinessCategoryList { get; set; }
    }
    /*public class BusinessCategory
    {
        public int Id { get; set; }

    }

    public class PhoneNos
    {
        public string PhoneNumber { get; set; }
    }

    public class ServiceArea
    {
        public string Name { get; set; }
    }*/
}
