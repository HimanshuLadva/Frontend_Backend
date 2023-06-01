using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Models
{
    public class WCMSUserProductFieldsModel
    {
        public int Id { get; set; }
        public int ProductsId { get; set; }
        public int ProductFieldsId { get; set; }
        public string FieldValue { get; set; }
        public bool IsBannerField { get; set; }
        public string? FieldName { get; set; }
        public string? Type { get; set; }
    }
}
