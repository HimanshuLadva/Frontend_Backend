using AngularAndNetCoreAuth.Models;
using Service.ViewModels;
using System.Threading.Tasks;

namespace AngularAndNetCoreAuth.Repo
{
    public interface IAuthServicesOld
    {
        Task<Response> FacebookLogin(TokenModel accessToken);
        Response GetResponse(ApplicationUser applicationUser, TokenModel idtoken);
        string GenerateTokens(ApplicationUser identityUser);
        string GenerateAccessToken(ApplicationUser identityUser);
        AspNetRefreshToken GenerateRefreshToken(string userId);

    }
}
