using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.DAL.Data.Interface
{
    public interface IBaseRepository
    {
        bool IsContextNull();
        string GetUserId();
        ApplicationUser GetUser();
        string GetImageBaseUrl();
        string GetBaseUrl();
    }
}
