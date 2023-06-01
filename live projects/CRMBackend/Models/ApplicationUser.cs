using CRMBackend.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CRMBackend.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        //[DataType(DataType.Text)]
        //public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsActive { get; set; } = true;
        public int ClientId { get; set; }
        public Clients? Client { get; set; }

    }
}
