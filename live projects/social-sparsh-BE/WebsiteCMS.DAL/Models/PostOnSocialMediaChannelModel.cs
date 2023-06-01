using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Models
{
    public class PostOnSocialMediaChannelModel
    {
        public IFormFile? CustomImage { get; set; }
        public string? templateId { get; set; }

        public string? Caption { get; set; }

        public List<string>? PlateformId { get; set; }
    }



    public class ConfirmationPostOnSocialMedialChannelModel
    {
        public string Id { get; set; }
        public string PlatForm { get; set; }

        public bool isImagePosted { get; set; }

        public string? ProfileName { get; set; }
    }
}
