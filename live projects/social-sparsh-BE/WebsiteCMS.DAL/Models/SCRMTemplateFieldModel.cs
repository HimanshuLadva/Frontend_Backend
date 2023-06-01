using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebsiteCMS.DAL.Models
{
    public class SCRMTemplateFieldModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Field Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Select Field Type")]
        public int TemplateFieldTypeId { get; set; }
        public string? FieldType { get; set; }
        public string? Value { get; set; }
        public IFormFile? FieldImage { get; set; }

        [Required(ErrorMessage = "Please Enter Field Value")]
        public bool IsActive { get; set; }
    }
}
