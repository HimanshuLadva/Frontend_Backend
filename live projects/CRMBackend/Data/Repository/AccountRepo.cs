using CRMBackend.Data.Interface;
using CRMBackend.Data.Models;
using CRMBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRMBackend.Data.Repository
{
    public class AccountRepo : IAccountRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RMbackendContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtBearerTokenSettings _jwtBearerTokenSettings;

        public AccountRepo(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IOptions<JwtBearerTokenSettings> jwtTokenOptions, RMbackendContext context, RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            _jwtBearerTokenSettings = jwtTokenOptions.Value;
        }

        public async Task<IdentityResult> SignUpAsyncForAdmin(SignUpModel model)
        {
            var data = new Clients()
            {
                Email = model.Email,
            };
            await _context.Clients.AddAsync(data);
            await _context.SaveChangesAsync();

            var client = await _context.Clients.Where(x => x.Email == model.Email).FirstOrDefaultAsync();
            var userData = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                ClientId = data!.Id
            };
            var result = await _userManager.CreateAsync(userData, model.Password);

            var user = new ApplicationUser();
            if (result.Succeeded)
            {
                user = await _userManager.FindByNameAsync(userData.Email);
                await _userManager.AddToRoleAsync(user, "Admin");
            }


            client!.Email = model.Email;
            client!.ApplicationUserId = user.Id;
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
            return result;
        }
        public async Task<IdentityResult> SignUpAsyncForUser(SignUpModel model)
        {
            var loggedInUser = await GetCurrentUserAsync();
            var client = await _context.Clients.Where(x => x.ApplicationUserId == loggedInUser.Id).FirstOrDefaultAsync();
            var userData = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                ClientId = client!.Id
            };
            var result = await _userManager.CreateAsync(userData, model.Password);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userData.Email);
                await _userManager.AddToRoleAsync(user, "User");
            }
            return result;
        }
        public async Task<UserViewModel> LoginAsync(SignInModel model)
        {
            UserViewModel userDetails = new();
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (!result.Succeeded)
            {
                return null!;
            }
            var user = await _userManager.FindByNameAsync(model.Email);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtBearerTokenSettings:SecretKey"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtBearerTokenSettings:Issuer"],
                audience: _configuration["JwtBearerTokenSettings:Audience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );


            userDetails.Token = new JwtSecurityTokenHandler().WriteToken(token);
            userDetails.Id = user.Id;
            userDetails.FirstName = user?.FirstName ?? string.Empty;
            userDetails.LastName = user?.LastName ?? string.Empty;
            userDetails.UserName = user?.UserName ?? string.Empty;
            userDetails.Role = userRoles.ToList();
            userDetails.Email = user?.Email ?? string.Empty;
            userDetails.SessionExpriryTime = _jwtBearerTokenSettings.ExpiryTimeInSeconds / 60;
            return userDetails;
        }

        public async Task<UserViewModel> MyAccountAsync(Claim claims)
        {
            string email = claims!.Value!.ToString();
            var data = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            var roles = await _context.UserRoles.Join(_roleManager.Roles, x => x.RoleId, y => y.Id, (x, y) => new { Id = x.UserId, Name = y.Name }).ToListAsync();
            var UserRoles = roles!.Where(x => x.Id == data!.Id).Select(x => x.Name).ToList();

            var userData = new UserViewModel()
            {
                Id = data!.Id,
                FirstName = data!.FirstName!,
                LastName = data!.LastName!,
                UserName = data!.UserName!,
                Email = data!.Email!,
                Role = UserRoles!
            };

            return userData!;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(_httpContextAccessor!.HttpContext!.User);
        }
    }
}
