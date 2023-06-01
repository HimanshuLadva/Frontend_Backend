using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.DAL.RequestModel.WCMSRequestModel
{
    public class WCMSRequest
    {
        public string[]? files;
        public WCMSTemplatePageFieldsModel[]? data;
    }
}
