using CRMBackend.Data.Interface;
using CRMBackend.Data.Models;
using CRMBackend.Models;
using Microsoft.AspNetCore.Identity;

namespace CRMBackend.Data.Repository
{
    public class AdministratorRepo : IAdministratorRepo
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RMbackendContext _dbContext;

        public AdministratorRepo(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, RMbackendContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task AddRoleAsync(string role)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = role
            };
            await _roleManager.CreateAsync(identityRole);
        }

        public async Task<ApplicationUser> AssignRole(ViewRoleModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return user;
            }
            else
            {
                await _userManager.UpdateAsync(user);
                await _userManager.AddToRoleAsync(user, model.Role);
            }
            return user;
        }
    }
}
