using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Models
{
    public class SCRMTemplateFieldTextModel
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string? TemplateName { get; set; }
        public int TemplateFieldId { get; set; }
        public string? TemplateFieldName { get; set; }
        public int FontFamilyId { get; set; }
        public string? FontFamilyName { get; set; }
        public string? Value { get; set; }
        public bool IsDisplay { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public int AlignId { get; set; }
        public string? Align { get; set; }
        public int Size { get; set; }
        public string Color { get; set; }
    }
}
