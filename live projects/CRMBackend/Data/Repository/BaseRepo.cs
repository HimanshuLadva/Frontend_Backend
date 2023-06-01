using CRMBackend.Data.Interface;
using CRMBackend.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CRMBackend.Data.Repository
{
    public class BaseRepo : IBaseRepo
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly UserManager<ApplicationUser> _userManager;
        public BaseRepo(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(_httpContextAccessor!.HttpContext!.User);
        }
    }
}
