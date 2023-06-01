using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Models
{
    public class SCRMUserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter User Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter User Email"), EmailAddress]
        public string Email { get; set; }
    }
}
