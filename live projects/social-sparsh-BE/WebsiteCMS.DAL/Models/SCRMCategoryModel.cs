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
    public class SCRMCategoryModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Please Enter Category Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Category Date")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Please Enter Category Status")]
        public bool IsActive { get; set; }

        public string? CategoryImageUrl { get; set; }
        public IFormFile? CategoryImage { get; set; }
        public ICollection<SCRMCaptionsModel>? Captions { get; set; }
    }

    public class SCRMMultipleCategorys
    {
        public string CategoryNames { get; set; }
    }

    public class SCRMCategoryWiseSubCategory
    {
        public int CategoryId { get; set; }

        public List<SCRMSubCategoryModel>? Data { get; set; }
    }
}
