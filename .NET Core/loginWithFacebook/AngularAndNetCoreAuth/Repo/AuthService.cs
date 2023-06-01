using AngularAndNetCoreAuth.Data;
using AngularAndNetCoreAuth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

namespace AngularAndNetCoreAuth.Repo
{
    public interface IAuthService
    {
        Task<Response> FacebookLogin(TokenModel accessToken);

        string GenerateTokens(ApplicationUser identityUser);

        string GenerateAccessToken(ApplicationUser identityUser);

        Response RefreshToken(string token);

        AspNetRefreshToken GetValidRefreshToken(string token, ApplicationUser identityUser);

        bool IsRefreshTokenValid(AspNetRefreshToken existingToken);

        AspNetRefreshToken GenerateRefreshToken(string userId);

        Response GetResponse(ApplicationUser applicationUser, TokenModel idtoken);
    }

    public class AuthService : IAuthService
    {
        private readonly UsersDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly JwtBearerTokenSettings jwtBearerTokenSettings;
        private readonly JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        private IHttpContextAccessor httpContextAccessor;

        public AuthService(IHttpContextAccessor _httpContextAccessor, IOptions<JwtBearerTokenSettings> _jwtTokenOptions, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> roleManager, UsersDbContext _dbContext)
        {
            dbContext = _dbContext;
            userManager = _userManager;
            jwtBearerTokenSettings = _jwtTokenOptions.Value;
            httpContextAccessor = _httpContextAccessor;
        }


        /// <summary>
        /// User login
        /// </summary>
        /// <param name="identityUser"></param>
        /// <param name="idtoken"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Facebook login
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<Response> FacebookLogin(TokenModel accessToken)
        {
            // 1.generate an app access token
            Response response = new Response();
            HttpClient client = new HttpClient();
            // 3. we've got a valid token so we can request user data from fb
            var userInfoResponse = await client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,first_name,last_name,middle_name,name,name_format,email&access_token={accessToken.Token}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);
            var info = new UserLoginInfo("FACEBOOK", userInfo.id, "FACEBOOK");
            var user = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            //var isexist = userManager.Users.FirstOrDefault(x => x.UserName == identityUser.UserName);

            try
            {
                if (user == null)
                {
                    var identityUser = new ApplicationUser()
                    {
                        UserName = userInfo.name,
                        FirstName = userInfo.first_name,
                        LastName = userInfo.last_name,
                        MiddleName = userInfo.middle_name,
                        NameFormat = userInfo.name_format,
                        Name = userInfo.name,
                        Email = userInfo.email,
                    };

                    var result = await userManager.CreateAsync(identityUser);
                    await userManager.AddLoginAsync(identityUser, info);
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

        /// <summary>
        /// Generate token
        /// </summary>
        /// <param name="identityUser"></param>
        /// <returns></returns>
        public string GenerateTokens(ApplicationUser identityUser)
        {
            Response response = new Response();            // Generate access token
            string accessToken = GenerateAccessToken(identityUser);

            // Generate refresh token and set it to cookie

            var refreshToken = GenerateRefreshToken(identityUser.Id);
            // Set Refresh Token Cookie
            var cookieOptions = new CookieOptions();

            cookieOptions.HttpOnly = true;
            cookieOptions.Expires = refreshToken.ExpiryOn;

            httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            // Save refresh token to database
            if (identityUser.RefreshTokens == null)
            {
                identityUser.RefreshTokens = new List<AspNetRefreshToken>();
            }

            var GetUserById = dbContext.Users.FirstOrDefault(x => x.Email == identityUser.Email);
            if (GetUserById != null)
            {
                refreshToken.ApplicationUserId = GetUserById.Id;
                refreshToken.UserAgent = identityUser.UserAgent;

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

        /// <summary>
        /// Generate access token
        /// </summary>
        /// <param name="identityUser"></param>
        /// <returns></returns>
        public string GenerateAccessToken(ApplicationUser identityUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);

            // var userRoles = userManager.GetRolesAsync(identityUser);
            var authclaim = new ClaimsIdentity(new Claim[]
                {
              new Claim(ClaimTypes.Name, identityUser.UserName.ToString()),
              //new Claim(ClaimTypes.Email, identityUser.Email),
              new Claim(ClaimTypes.NameIdentifier, identityUser.Id),
              new Claim(ClaimTypes.PrimarySid, identityUser.UserId.ToString()),
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
            //    ExpiryOn = DateTime.UtcNow.AddSeconds(jwtBearerTokenSettings.ExpiryTimeInSeconds);
            //}

            ExpiryOn = DateTime.UtcNow.AddSeconds(jwtBearerTokenSettings.ExpiryTimeInSeconds);

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

        /// <summary>
        /// Generate access token
        /// </summary>
        /// <param name="identityUser"></param>
        /// <returns></returns>
        public string GenerateAccessTokenAdvisor(ApplicationUser identityUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);

            var authclaim = new ClaimsIdentity(new Claim[]
                {
              new Claim(ClaimTypes.Name, identityUser.UserName.ToString()),
              new Claim(ClaimTypes.NameIdentifier, identityUser.Id),
              new Claim(ClaimTypes.PrimarySid, identityUser.UserId.ToString()),
                });

            DateTime? ExpiryOn = dbContext.AspNetRefreshTokens.Where(x => x.ApplicationUserId == identityUser.Id).Select(x => x.ExpiryOn).FirstOrDefault();

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

        public Response RefreshToken(string token)
        {
            Response response = new Response();
            //
            var identityUser = dbContext.Users.Include(x => x.RefreshTokens)
                .FirstOrDefault(x => x.RefreshTokens.Any(y => y.Token == token && y.ApplicationUserId == x.Id));
            // Get existing refresh token if it is valid and revoke it
            var existingRefreshToken = GetValidRefreshToken(token, identityUser);
            if (existingRefreshToken == null)
            {
                response.Message = "Failed";
                response.Success = false;
                return response;
            }
            existingRefreshToken.RevokedByIp = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
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
            var user = userManager.FindByIdAsync(userId).Result;

            //var roleName = userManager.GetRolesAsync(user).Result.FirstOrDefault();

            DateTime ExpiryOn;
            //if (roleName == "advisor" && user.ExpiryTime != null)
            //{
            //    ExpiryOn = DateTime.UtcNow.AddSeconds(Convert.ToDouble(user.ExpiryTime));
            //}
            //else
            //{
            //    ExpiryOn = DateTime.UtcNow.AddDays(jwtBearerTokenSettings.ExpiryTimeInDays);
            //}

            ExpiryOn = DateTime.UtcNow.AddDays(jwtBearerTokenSettings.ExpiryTimeInDays);

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
