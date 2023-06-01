using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? UserAgent { get; set; }
        public string? FirstName { get; set; }
        //public long UserId { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Name { get; set; }
        public string? NameFormat { get; set; }
        public string? Email { get; set; }

        //public AspNetUserProfile AspNetUserProfile { get; set; }

        public ICollection<BOTChatBot> ChatBot { get; set; }

        public ICollection<SCRMUserMetaData> SCRMUserMetaData { get; set; }
        public ICollection<WCMSUserTemplates> UserTemplates { get; set; }
        public List<AspNetRefreshToken> RefreshTokens { get; set; }
    }
}
