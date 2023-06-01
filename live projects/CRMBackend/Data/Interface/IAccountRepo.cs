using CRMBackend.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CRMBackend.Data.Interface
{
    public interface IAccountRepo
    {
        //Task AddClient(SignUpModel model);
        Task<IdentityResult> SignUpAsyncForAdmin(SignUpModel model);
        Task<UserViewModel> LoginAsync(SignInModel model);
        Task LogOut();
        Task<UserViewModel> MyAccountAsync(Claim claims);
        Task<IdentityResult> SignUpAsyncForUser(SignUpModel model);
        //Task<List<string>> GetAllCountryStateAndCity();
    }
}
