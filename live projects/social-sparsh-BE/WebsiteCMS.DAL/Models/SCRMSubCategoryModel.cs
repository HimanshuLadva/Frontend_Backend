using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;

namespace WebsiteCMS.DAL.Models
{
    public class SCRMSubCategoryModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Please Enter Category Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Category Date")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Please Enter Category Status")]
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? SubCategoryImageURL { get; set; }
        public IFormFile? SubCategoryImage { get; set; }
        public ICollection<SCRMCaptionsModel>? Captions { get; set; }
    }
}
