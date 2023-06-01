using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.Util;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class AdministratorRepository : IAdministratorRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly WebsiteCMSDbContext _dbContext;

        public AdministratorRepository(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, WebsiteCMSDbContext dbContext)
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
            //await _dbContext.Users.FirstOrDefaultAsync(x => x.Role == model.Role);
            if(user== null) {
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
