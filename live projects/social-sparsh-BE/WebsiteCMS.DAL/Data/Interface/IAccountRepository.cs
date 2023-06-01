using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Security.Claims;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.AuthRequestModel;

namespace WebsiteCMS.DAL.Data.Interface
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of entity Type <see cref="ApplicationUser"/>.
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// Signs the up async.
        ///   <para>
        ///     Gets the model of type <see cref="SignUpModel"/>.
        /// </para>
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="IdentityResult"/>.</returns>
        Task<IdentityResult> SignUpAsync(SignUpModel model);

        /// <summary>
        /// Logins the async.
        ///   <para>
        ///     Gets the model of type <see cref="SignInModel"/>.
        /// </para>
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="VM_UserDetails"/>.</returns>
        Task<VM_UserDetails> LoginAsync(SignInModel model);

        /// <summary>
        /// Logs the out.
        /// </summary>
        /// <returns>A Task.</returns>
        Task LogOut();

        /// <summary>
        /// Facebooks the login.
        ///   <para>
        ///     Gets the model of type <see cref="TokenModel"/>.
        /// </para>
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="AuthResponse"/>.</returns>
        Task<AuthResponse> FacebookLogin(TokenModel accessToken);

        /// <summary>
        /// Generates the tokens.
        ///   <para>
        ///     Gets the model of type <see cref="ApplicationUser"/>.
        /// </para>
        /// </summary>
        /// <param name="identityUser">The identity user.</param>
        /// <returns>Returns a Entity type <see cref="string"/>.</returns>
        string GenerateTokens(ApplicationUser identityUser);

        /// <summary>
        /// Generates the access token.
        ///   <para>
        ///     Gets the model of type <see cref="ApplicationUser"/>.
        /// </para>
        /// </summary>
        /// <param name="identityUser">The identity user.</param>
        /// <returns>Returns a Entity type <see cref="string"/>.</returns>
        string GenerateAccessToken(ApplicationUser identityUser);

        /// <summary>
        /// Refreshes the token.
        ///    <para>
        ///     Gets The <see cref="string"/> token that we get from facebook login.
        /// </para>
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Returns a Entity type <see cref="AuthResponse"/>.</returns>
        AuthResponse RefreshToken(string token);

        /// <summary>
        /// Gets the valid refresh token.
        ///     <para>
        ///     Gets The <see cref="string"/> token that we get from facebook login.
        /// </para>
        ///   <para>
        ///     Gets the model of type <see cref="ApplicationUser"/>.
        /// </para>
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="identityUser">The identity user.</param>
        /// <returns>Returns a Entity type <see cref="AspNetRefreshToken"/>.</returns>
        AspNetRefreshToken GetValidRefreshToken(string token, ApplicationUser identityUser);

        /// <summary>
        /// Are the refresh token valid.
        ///   <para>
        ///     Gets the model of type <see cref="AspNetRefreshToken"/>.
        /// </para>
        /// </summary>
        /// <param name="existingToken">The existing token.</param>
        /// <returns>Returns a <see cref="Boolean"/> indecating Success of the operation.</returns>
        bool IsRefreshTokenValid(AspNetRefreshToken existingToken);

        /// <summary>
        /// Generates the refresh token.
        ///     <para>
        ///     Gets The <see cref="string"/> userId is Current logged user Id.
        /// </para>
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>Returns a Entity type <see cref="AspNetRefreshToken"/>.</returns>
        AspNetRefreshToken GenerateRefreshToken(string userId);

        /// <summary>
        /// Gets the response.
        ///   <para>
        ///     Gets the model of type <see cref="ApplicationUser"/>.
        /// </para>
        ///   <para>
        ///     Gets the model of type <see cref="TokenModel"/>.
        /// </para>
        /// </summary>
        /// <param name="applicationUser">The application user.</param>
        /// <param name="idtoken">The idtoken.</param>
        /// <returns>Returns a Entity type <see cref="AuthResponse"/>.</returns>
        AuthResponse GetResponse(ApplicationUser applicationUser, TokenModel idtoken);

        /// <summary>
        /// Mies the account async.
        ///   <para>
        ///     Gets the model of type <see cref="Claim"/>.
        /// </para>
        /// </summary>
        /// <param name="claims">The claims.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="UserViewModel"/>.</returns>
        Task<UserViewModel> MyAccountAsync(Claim claims);

        /// <summary>
        /// Gets the user details by email id.
        /// <para>
        ///     Gets The <see cref="string"/> email Id of any user.
        /// </para>
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="VM_UserDetails"/>.</returns>
        Task<VM_UserDetails> GetUserDetailsByEmailId(string email);
    }
}
