using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Models
{
    public class SCRMUserMetaDataModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter User Id")]
        public string ApplicationUserId { get; set; }

        [Required(ErrorMessage = "Please Enter TemplateField Id")]
        public int TemplateFieldId { get; set; }
        public string FieldType { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter TemplateField Value")]
        public string Value { get; set; }
    }
}
