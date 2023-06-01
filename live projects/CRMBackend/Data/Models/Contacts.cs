namespace CRMBackend.Data.Models
{
    public class Contacts
    {
        // Company Detail
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyAddress { get; set; }
        public int StateId { get; set; }
        public States? State { get; set; }
        public int DistrictId { get; set; }
        public Districts? District { get; set; }
        public int CityId { get; set; }
        public Cities? City { get; set; }
        public string? PinCode { get; set; }
        public string? CompanyPhoneNo1 { get; set; }
        public string? CompanyPhoneNo2 { get; set; }
        public string? CompanyWebsite { get; set; }
        public string? CompanyEmail { get; set; }

        //important dates
        public DateTime? ImportantDate { get; set; }
        public string? ImportantDateDesc { get; set; }

        //person detail 
        public string? PersonName { get; set; }
        public string? PersonDesignation { get; set; }
        public DateTime? PersonBirthday { get; set; }
        public string? PersonMobile { get; set; }
        public string? PersonAddress { get; set; }
        public int PersonStateId { get; set; }
        public int PersonDistrictId { get; set; }
        public int PersonCityId { get; set; }
        public string? PersonPinCode { get; set; }
        public string? PersonPhoneNo1 { get; set; }
        public string? PersonPhoneNo2 { get; set; }
        public string? PersonEmail { get; set; }
        public string? Person2Name { get; set; }
        public string? Person2Designation { get; set; }
        public DateTime? Person2Birthday { get; set; }
        public string? Person2Mobile { get; set; }

        // references
        public int ClientId { get; set; }
        public Clients? Client { get; set; }

        public ICollection<UserPhotos>? UserPhotos { get; set; }
        public ICollection<UserNotes>? UserNotes { get; set; }
        public ICollection<ContactEvents>? ContactEvent { get; set; }
        public ICollection<ContactGroups>? ContactGroup { get; set; }
    }
}
