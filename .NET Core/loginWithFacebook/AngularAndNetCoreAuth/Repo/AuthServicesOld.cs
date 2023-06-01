using AngularAndNetCoreAuth.Data;
using AngularAndNetCoreAuth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Service.Models;
using Service.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static AngularAndNetCoreAuth.Repo.AuthServicesOld;

namespace AngularAndNetCoreAuth.Repo
{
    public class AuthServicesOld: IAuthServices
    {
        private readonly UsersDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        private readonly JwtBearerTokenSettings jwtBearerTokenSettings;
        private IHttpContextAccessor httpContextAccessor;

        public AuthServicesOld(IOptions<JwtBearerTokenSettings> _jwtTokenOptions, UsersDbContext dbContext, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            jwtBearerTokenSettings = _jwtTokenOptions.Value;
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

      
        public async Task<Response> FacebookLogin(TokenModel accessToken)
        
        {
            // 1.generate an app access token
            Response response = new Response();
            HttpClient client = new HttpClient();
            // 3. we've got a valid token so we can request user data from fb
            var userInfoResponse = await client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name&access_token={accessToken.Token}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);
            var info = new UserLoginInfo("FACEBOOK", userInfo.id, userInfo.first_name);
            var user = userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            var identityUser = new ApplicationUser() { UserName = userInfo.first_name };
            //var isexist = userManager.Users.FirstOrDefault(x => x.UserName == identityUser.UserName);
            if (user == null)
            {
                var result = await userManager.CreateAsync(identityUser);
                var addeduser = await userManager.AddLoginAsync(identityUser, info);
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
            }
            else
            {
                var res = GetResponse(identityUser, accessToken);
                return res;
            }
            return response;
        }
        public Response GetResponse(ApplicationUser identityUser, TokenModel idtoken)
        {
            Response response = new Response();
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
            Response response = new Response();            // Generate access token

            string accessToken =  GenerateAccessToken(identityUser).ToString();

            // Generate refresh token and set it to cookie

            var refreshToken = GenerateRefreshToken(identityUser.Id.ToString());
            // Set Refresh Token Cookie
            var cookieOptions = new CookieOptions();

            cookieOptions.HttpOnly = true;
            cookieOptions.Expires = refreshToken.ExpiryOn;

            httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            // Save refresh token to database
            if (identityUser.RefreshTokens == null)
            {
                identityUser.RefreshTokens =(new List<AspNetRefreshToken>()).ToString();
            }
            var GetUserById = dbContext.Users.FirstOrDefault(x => x.Email == identityUser.Email);
            if (GetUserById != null)
            {
                refreshToken.ApplicationUserId = GetUserById.Id;
                refreshToken.UserAgent = identityUser.UserAgent.ToString();

                dbContext.AspNetRefreshTokens.Update(refreshToken);
                bool hasChanges = dbContext.ChangeTracker.HasChanges();
                dbContext.SaveChanges();
            }
            else
            {
                refreshToken.ApplicationUserId = GetUserById.Id;
                refreshToken.UserAgent = identityUser.UserAgent;
                dbContext.AspNetRefreshTokens.Add(refreshToken);
                bool hasChanges = dbContext.ChangeTracker.HasChanges();
                dbContext.SaveChanges();
            }
            return accessToken;
        }

        public string GenerateAccessToken(ApplicationUser identityUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey?.ToString());

            // var userRoles = userManager.GetRolesAsync(identityUser);
            var authclaim = new ClaimsIdentity(new Claim[]
                {
              new Claim(ClaimTypes.Name, identityUser.UserName.ToString()),
              //new Claim(ClaimTypes.Email, identityUser.Email),
              new Claim(ClaimTypes.NameIdentifier, identityUser.Id.ToString()),
              new Claim(ClaimTypes.PrimarySid, identityUser.UserId.ToString()),
                });
            //foreach (var item in userRoles.Result) {
            //    authclaim.AddClaim(new Claim(ClaimTypes.Role, item));
            //}

            string roleName = string.Empty;

            //try
            //{
            //    roleName = userManager.GetRolesAsync(identityUser).Result.FirstOrDefault();
            //}
            //catch(Exception ex) 
            //{
            //    throw new NotImplementedException(ex.Message);
            //}

            DateTime ExpiryOn;
            if (!string.IsNullOrEmpty(roleName) && (roleName == "advisor" && identityUser.ExpiryTime != null))
            {
                ExpiryOn = DateTime.UtcNow.AddSeconds(Convert.ToDouble(identityUser.ExpiryTime));
            }
            else
            {
                ExpiryOn = DateTime.UtcNow.AddSeconds(jwtBearerTokenSettings.ExpiryTimeInSeconds);
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = authclaim,
                Expires = ExpiryOn,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = jwtBearerTokenSettings.Audience,
                Issuer = jwtBearerTokenSettings.Issuer
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public AspNetRefreshToken GenerateRefreshToken(string userId)
        {
            var user = userManager.FindByIdAsync(userId).Result;

            var roleName = userManager.GetRolesAsync(user).Result.FirstOrDefault();

            DateTime ExpiryOn;
            if (roleName == "advisor" && user.ExpiryTime != null)
            {
                ExpiryOn = DateTime.UtcNow.AddSeconds(Convert.ToDouble(user.ExpiryTime));
            }
            else
            {
                ExpiryOn = DateTime.UtcNow.AddDays(jwtBearerTokenSettings.ExpiryTimeInDays);
            }

            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new AspNetRefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    ExpiryOn = ExpiryOn,
                    CreatedOn = DateTime.UtcNow,
                    CreatedByIp = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                    ApplicationUserId = userId
                };
            }
        }
    }
}
