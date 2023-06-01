using System;

namespace AngularAndNetCoreAuth.Models
{
    public partial class AspNetRefreshToken
    {
        public int Id { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpiryOn { get; set; }

        public DateTime? CreatedOn { get; set; }
        public string? CreatedByIp { get; set; }
        public DateTime? RevokedOn { get; set; }
        public string? RevokedByIp { get; set; }
        public string? UserAgent { get; set; }
        public string? ApplicationUserId { get; set; }
    }

    public partial class AspNetUserProfile
    {
        public int Id { get; set; }
        public string? Gender { get; set; }
        public string? AboutMe { get; set; }
        public string? PhoneCode { get; set; }

    }
}
