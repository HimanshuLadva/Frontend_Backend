using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly UserManager<ApplicationUser> _userManager;
        public BaseRepository(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public bool IsContextNull()
        {
            if (_httpContextAccessor!.HttpContext == null)
            {
                return true;
            }
            return false;
        }
        public string GetUserId()
        {
            string userId = string.Empty;

            if (!IsContextNull())
            {
                userId = _httpContextAccessor!.HttpContext!.User!.Claims.First(c => c.Type ==  ClaimTypes.NameIdentifier).Value;
            }

            return userId;
        }

        public ApplicationUser GetUser()
        {
            ApplicationUser user = null;

            if (!IsContextNull())
            {
                var userId = GetUserId();
                if (!string.IsNullOrEmpty(userId))
                {
                    if (_userManager != null && _userManager.Users != null)
                    {
                        user = _userManager.Users.FirstOrDefault(x => x.Id == userId);
                    }
                }
            }

            return user!;
        }

        public string GetBaseUrl()
        {
            string baseUrl = string.Empty;

            if (!IsContextNull())
            {
                var curRequest = _httpContextAccessor?.HttpContext?.Request;
                if (curRequest != null)
                {
                    baseUrl = string.Format("{0}{1}{2}", curRequest.Scheme, Uri.SchemeDelimiter, curRequest.Host.Value);
                }

                if (!string.IsNullOrEmpty(baseUrl))
                {
                    baseUrl = baseUrl.TrimEnd('/');
                }
            }

            return baseUrl;
        }

        public string GetImageBaseUrl()
        {
            return "https://social-sparsh.s3.amazonaws.com/";
        }
    }
}
