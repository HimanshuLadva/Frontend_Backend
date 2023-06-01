using CRMBackend.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace CRMBackend.Models
{
    public class ContactModel
    {
        public int Id { get; set; }
        [Required]
        public string? CompanyName { get; set; }
        [Required]
        public string? CompanyAddress { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public int DistrictId { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public string? PinCode { get; set; }
        [Required]
        public string? CompanyPhoneNo1 { get; set; }
        public string? CompanyPhoneNo2 { get; set; }
        [Required]
        public string? CompanyWebsite { get; set; }
        [Required]
        public string? CompanyEmail { get; set; }

        //important dates
        public DateTime? ImportantDate { get; set; }
        public string? ImportantDateDesc { get; set; }

        //person detail 
        [Required]
        public string? PersonName { get; set; }
        [Required]
        public string? PersonDesignation { get; set; }
        [Required]
        public DateTime? PersonBirthday { get; set; }
        [Required]
        public string? PersonMobile { get; set; }
        public string? PersonAddress { get; set; }
        [Required]
        public int PersonStateId { get; set; }
        [Required]
        public int PersonDistrictId { get; set; }
        [Required]
        public int PersonCityId { get; set; }
        [Required]
        public string? PersonPinCode { get; set; }
        [Required]
        public string? PersonPhoneNo1 { get; set; }
        public string? PersonPhoneNo2 { get; set; }
        [Required]
        public string? PersonEmail { get; set; }
        public string? Person2Name { get; set; }
        public string? Person2Designation { get; set; }
        public DateTime? Person2Birthday { get; set; }
        public string? Person2Mobile { get; set; }

        // references
        public int ClientId { get; set; }

        //public IFormFile? Image { get; set; }
        //public string? ImageUrl { get; set; }
        //public string? Description { get; set; }
        //public IFormFile? Document { get; set; }
        //public string? DocumentUrl { get; set; }
        //[Required]
        //public int ContactsId { get; set; }

    }
}
