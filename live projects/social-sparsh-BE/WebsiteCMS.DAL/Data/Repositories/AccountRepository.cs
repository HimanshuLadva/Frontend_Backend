
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Crmf;
using RestSharp;
using System;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.AuthRequestModel;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly WebsiteCMSDbContext _websiteCMSDbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtBearerTokenSettings _jwtBearerTokenSettings;
        private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();
        private IHttpContextAccessor _httpContextAccessor;
        private IBaseRepository _baseRepository;
        private readonly FacebookSettingsModel _fbSettings;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
         , IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IOptions<JwtBearerTokenSettings> jwtTokenOptions, WebsiteCMSDbContext webContext, RoleManager<IdentityRole> roleManager, IBaseRepository baseRepository, IOptions<FacebookSettingsModel> fbSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _jwtBearerTokenSettings = jwtTokenOptions.Value;
            _websiteCMSDbContext = webContext;
            _roleManager = roleManager;
            _baseRepository = baseRepository;
            _fbSettings = fbSettings.Value;
        }
        public SignInModel? Storedata { get; set; }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var userData = new ApplicationUser()
            {
                //UserId = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
            };
            var result = await _userManager.CreateAsync(userData, model.Password);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userData.Email);
                await _userManager.AddToRoleAsync(user, "User");
            }
            return result;
        }

        public async Task<VM_UserDetails?> LoginAsync(SignInModel model)
        {
            VM_UserDetails userDetails = new();
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (!result.Succeeded)
            {
                return null;
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
                 expires: DateTime.Now.AddHours(_jwtBearerTokenSettings.ExpiryTimeInSeconds),
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
            userDetails.IsBrandInfoAvailable = _websiteCMSDbContext.tblSCRMUserMetaData.Where(x => x.ApplicationUserId == user!.Id).Count() > 0 ? true : false;
            return userDetails;
        }

        public async Task<VM_UserDetails> GetUserDetailsByEmailId(string email)
        {
            VM_UserDetails userDetails = new();

            var user = await _userManager.FindByNameAsync(email);
            var userRoles = await _userManager.GetRolesAsync(user);

            userDetails.Id = user.Id;
            userDetails.FirstName = user?.FirstName ?? string.Empty;
            userDetails.LastName = user?.LastName ?? string.Empty;
            userDetails.UserName = user?.UserName ?? string.Empty;
            userDetails.Role = userRoles.ToList();
            userDetails.Email = user?.Email ?? string.Empty;
            userDetails.SessionExpriryTime = _jwtBearerTokenSettings.ExpiryTimeInSeconds / 60;
            userDetails.IsBrandInfoAvailable = _websiteCMSDbContext.tblSCRMUserMetaData.Where(x => x.ApplicationUserId == user!.Id).Count() > 0 ? true : false;
            return userDetails;
        }


        public async Task<UserViewModel> MyAccountAsync(Claim claims)
        {
            string email = claims!.Value!.ToString();
            var data = await _websiteCMSDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            var roles = await _websiteCMSDbContext.UserRoles.Join(_roleManager.Roles, x => x.RoleId, y => y.Id, (x, y) => new { Id = x.UserId, Name = y.Name }).ToListAsync();
            var UserRoles = roles!.Where(x => x.Id == data!.Id).Select(x => x.Name).ToList();

            var userData = new UserViewModel()
            {
                Id = data!.Id,
                FirstName = data!.FirstName!,
                LastName = data!.LastName!,
                UserName = data!.UserName!,
                Email = data!.Email!,
                Role = UserRoles
            };

            return userData!;
        }
        public async Task<AuthResponse> FacebookLogin(TokenModel accessToken)
        {
            // 1.generate an app access token
            AuthResponse response = new AuthResponse();
            HttpClient client = new HttpClient();
            // 3. we've got a valid token so we can request user data from fb
            var userInfoResponse = await client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,first_name,last_name,middle_name,name,name_format,email&access_token={accessToken.accessToken}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);
            var info = new UserLoginInfo("FACEBOOK", userInfo!.id, "FACEBOOK");
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            //var isexist = userManager.Users.FirstOrDefault(x => x.UserName == identityUser.UserName);

            var LongLivedToken = await client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id={_fbSettings.AppId}&client_secret={_fbSettings.AppSecret}&fb_exchange_token={accessToken.accessToken}");

            try
            {
                if (user == null)
                {
                    var identityUser = new ApplicationUser()
                    {
                        //UserId = Convert.ToInt64(userInfo.id),
                        UserName = userInfo.email,
                        FirstName = userInfo.first_name,
                        LastName = userInfo.last_name,
                        Email = userInfo.email,
                    };

                    var result = await _userManager.CreateAsync(identityUser);
                    await _userManager.AddToRoleAsync(identityUser, "User");
                    await _userManager.AddLoginAsync(identityUser, info);
                    if (!result.Succeeded)
                    {
                        Dictionary<string, string> valuePairs = new Dictionary<string, string>();
                        foreach (IdentityError error in result.Errors)
                        {
                            valuePairs.Add(error.Code, error.Description);
                        }
                        response.Success = false;
                        response.Message = "User Registration Failed";
                        response.Data = valuePairs;
                    }
                    else
                    {
                        var res = GetResponse(identityUser, accessToken);
                        return res;
                    }
                }
                else
                {
                    var res = GetResponse(user, accessToken);
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw;
                //throw new Exception;
            }
            return response;

        }
        public AuthResponse GetResponse(ApplicationUser identityUser, TokenModel idtoken)
        {
            AuthResponse response = new AuthResponse();
            var tokenDescriptor = GenerateTokens(identityUser);
            if (tokenDescriptor != null)
            {
                UserLoginModel userLoginModel = new UserLoginModel();
                userLoginModel.UserName = identityUser.UserName;
                //faceBookLoginModel.UserId = identityUser.Id;
                userLoginModel.Email = identityUser.Email;
                userLoginModel.AccessToken = tokenDescriptor;
                userLoginModel.RememberMe = idtoken.RememberMe;

                response.Data = userLoginModel;
                response.Message = "User LoggedIn successfully.";
                response.Success = true;
                return response;
            }
            else
            {
                response.Success = false;
                response.Message = "Token is not generated.";
                return response;
            }
        }

        public string GenerateTokens(ApplicationUser identityUser)
        {
            AuthResponse response = new AuthResponse();            // Generate access token
            string accessToken = GenerateAccessToken(identityUser);

            // Generate refresh token and set it to cookie

            var refreshToken = GenerateRefreshToken(identityUser.Id);
            // Set Refresh Token Cookie
            var cookieOptions = new CookieOptions();

            cookieOptions.HttpOnly = true;
            cookieOptions.Expires = refreshToken.ExpiryOn;

            _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            // Save refresh token to database
            if (identityUser.RefreshTokens == null)
            {
                identityUser.RefreshTokens = new List<AspNetRefreshToken>();
            }

            var GetUserById = _websiteCMSDbContext.Users.FirstOrDefault(x => x.Email == identityUser.Email);
            if (GetUserById != null)
            {
                refreshToken.ApplicationUserId = GetUserById.Id;
                refreshToken.UserAgent = identityUser.UserAgent;

                _websiteCMSDbContext.AspNetRefreshTokens.Update(refreshToken);
                bool hasChanges = _websiteCMSDbContext.ChangeTracker.HasChanges();
                _websiteCMSDbContext.SaveChanges();
            }
            else
            {
                refreshToken.ApplicationUserId = GetUserById.Id;
                refreshToken.UserAgent = identityUser.UserAgent;
                _websiteCMSDbContext.AspNetRefreshTokens.Add(refreshToken);
                bool hasChanges = _websiteCMSDbContext.ChangeTracker.HasChanges();
                _websiteCMSDbContext.SaveChanges();
            }
            return accessToken;
        }

        public string GenerateAccessToken(ApplicationUser identityUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtBearerTokenSettings.SecretKey);

            // var userRoles = userManager.GetRolesAsync(identityUser);
            var authclaim = new ClaimsIdentity(new Claim[]
                {
              new Claim(ClaimTypes.Name, identityUser.UserName.ToString()),
              //new Claim(ClaimTypes.Email, identityUser.Email),
              new Claim(ClaimTypes.NameIdentifier, identityUser.Id),
              //new Claim(ClaimTypes.PrimarySid, identityUser.UserId.ToString()),
                });
            //foreach (var item in userRoles.Result) {
            //    authclaim.AddClaim(new Claim(ClaimTypes.Role, item));
            //}

            //var roleName = userManager.GetRolesAsync(identityUser).Result.FirstOrDefault();

            DateTime ExpiryOn;
            //if (roleName == "advisor" && identityUser.ExpiryTime != null)
            //{
            //    ExpiryOn = DateTime.UtcNow.AddSeconds(Convert.ToDouble(identityUser.ExpiryTime));
            //}
            //else
            //{
            //    ExpiryOn = DateTime.UtcNow.AddSeconds(_jwtBearerTokenSettings.ExpiryTimeInSeconds);
            //}

            ExpiryOn = DateTime.UtcNow.AddSeconds(_jwtBearerTokenSettings.ExpiryTimeInSeconds);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = authclaim,
                Expires = ExpiryOn,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtBearerTokenSettings.Audience,
                Issuer = _jwtBearerTokenSettings.Issuer
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateAccessTokenAdvisor(ApplicationUser identityUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtBearerTokenSettings.SecretKey);

            var authclaim = new ClaimsIdentity(new Claim[]
                {
              new Claim(ClaimTypes.Name, identityUser.UserName.ToString()),
              new Claim(ClaimTypes.NameIdentifier, identityUser.Id),
                    //new Claim(ClaimTypes.PrimarySid, identityUser.UserId.ToString()),
                });

            DateTime? ExpiryOn = _websiteCMSDbContext.AspNetRefreshTokens.Where(x => x.ApplicationUserId == identityUser.Id).Select(x => x.ExpiryOn).FirstOrDefault();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = authclaim,
                Expires = ExpiryOn,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtBearerTokenSettings.Audience,
                Issuer = _jwtBearerTokenSettings.Issuer
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public AuthResponse RefreshToken(string token)
        {
            AuthResponse response = new AuthResponse();
            //
            var identityUser = _websiteCMSDbContext.Users.Include(x => x.RefreshTokens)
                .FirstOrDefault(x => x.RefreshTokens.Any(y => y.Token == token && y.ApplicationUserId == x.Id));
            // Get existing refresh token if it is valid and revoke it
            var existingRefreshToken = GetValidRefreshToken(token, identityUser);
            if (existingRefreshToken == null)
            {
                response.Message = "Failed";
                response.Success = false;
                return response;
            }
            existingRefreshToken.RevokedByIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            existingRefreshToken.RevokedOn = DateTime.UtcNow;
            // Generate new tokens
            var tokenDescriptor = GenerateTokens(identityUser);
            if (tokenDescriptor == null)
            {
                response.Message = "Token is not generated.";
                response.Success = false;
                return response;
            }
            else
            {
                response.Message = "Success";
                response.Success = true;
                response.Data = new
                {
                    //RefreshToken = val.refreshToken,
                    AccessToken = tokenDescriptor
                };
                return response;
            }
        }

        public AspNetRefreshToken GetValidRefreshToken(string token, ApplicationUser identityUser)
        {
            if (identityUser == null)
            {
                return null;
            }
            var existingToken = identityUser.RefreshTokens.FirstOrDefault(x => x.Token == token);
            return IsRefreshTokenValid(existingToken) ? existingToken : null;
        }

        public bool IsRefreshTokenValid(AspNetRefreshToken existingToken)
        {
            // Is token already revoked, then return false
            if (existingToken.RevokedByIp != null && existingToken.RevokedOn != DateTime.MinValue)
            {
                return false;
            }
            // Token already expired, then return false
            if (existingToken.ExpiryOn <= DateTime.UtcNow)
            {
                return false;
            }
            return true;
        }

        public AspNetRefreshToken GenerateRefreshToken(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;

            //var roleName = userManager.GetRolesAsync(user).Result.FirstOrDefault();

            DateTime ExpiryOn;
            //if (roleName == "advisor" && user.ExpiryTime != null)
            //{
            //    ExpiryOn = DateTime.UtcNow.AddSeconds(Convert.ToDouble(user.ExpiryTime));
            //}
            //else
            //{
            //    ExpiryOn = DateTime.UtcNow.AddDays(_jwtBearerTokenSettings.ExpiryTimeInDays);
            //}

            ExpiryOn = DateTime.UtcNow.AddDays(_jwtBearerTokenSettings.ExpiryTimeInDays);

            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new AspNetRefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    ExpiryOn = ExpiryOn,
                    CreatedOn = DateTime.UtcNow,
                    CreatedByIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                    ApplicationUserId = userId
                };
            }
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

    }
}
