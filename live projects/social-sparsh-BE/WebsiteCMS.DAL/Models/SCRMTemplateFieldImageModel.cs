using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Models
{
    public class SCRMTemplateFieldImageModel
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string? TemplateName { get; set; }
        public int TemplateFieldId { get; set; }
        public string? TemplateFieldName { get; set; }
        public string? Value { get; set; }
        public bool IsDisplay { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
