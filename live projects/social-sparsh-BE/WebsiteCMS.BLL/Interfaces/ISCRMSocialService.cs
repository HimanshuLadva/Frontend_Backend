using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.AuthRequestModel;

namespace WebsiteCMS.BLL.Interfaces
{
    /// <summary>
    ///     An interface that contains the Methods to interact with a table of many entity Type.
    /// </summary>
    public interface ISCRMSocialService
    {

        /// <summary>
        /// Facebooks the long lived token.
        ///  <para>
        ///     Gets The <see cref="string"/> accessToken of facebook account.
        /// </para>
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="string"/>.</returns>
        Task<string> FacebookLongLivedToken(string accessToken);

        /// <summary>
        /// FS the b pages and tokens.
        /// <para>
        ///     Gets the model of type <see cref="TokenModel"/>.
        /// </para>
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> FBPagesAndTokens(TokenModel accessToken);

        /// <summary>
        /// FS the b post image.
        ///   <para>
        ///     Gets The <see cref="string"/> PageName of facebook.
        /// </para>
        ///   <para>
        ///     Gets The <see cref="string"/> Caption that are write under post.
        /// </para>
        ///   <para>
        ///     Gets The <see cref="IFormFile"/> Image that post on facebook.
        /// </para>
        /// </summary>
        /// <param name="PageName">The page name.</param>
        /// <param name="Caption">The caption.</param>
        /// <param name="image">The image.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="int"/>.</returns>
        Task<int> FBPostImage(string PageName, string Caption, IFormFile image);

        /// <summary>
        /// IS the g post image.
        ///  <para>
        ///     Gets the model of type <see cref="TokenModel"/>.
        /// </para>
        ///    <para>
        ///     Gets The <see cref="string"/> imagePath that post on facebook.
        /// </para>
        ///    <para>
        ///     Gets The <see cref="string"/> Caption that are write under post.
        /// </para>
        /// </summary>
        /// <param name="tokenModel">The token model.</param>
        /// <param name="imagePath">The image path.</param>
        /// <param name="caption">The caption.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="Boolean"/> indecating Success of the operation.</returns>
        Task<bool> IGPostImage(TokenModel tokenModel, string imagePath, string caption);

        /// <summary>
        /// Fb page list.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="AllFBPageAndLinkedInstagramAccountModel"/>.</returns>
        Task<List<AllFBPageAndLinkedInstagramAccountModel>> FBPageList();

        /// <summary>
        /// Gets the facebook page details.
        /// <para>
        ///     Gets The <see cref="long"/> PageId of facebook page.
        /// </para>
        ///  <para>
        ///     Gets The <see cref="string"/> PageAccessToken of facebook page.
        /// </para>
        /// </summary>
        /// <param name="PageId">The page id.</param>
        /// <param name="pageAccessToken">The page access token.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="FacebookPageDetailModel"/>.</returns>
        Task<FacebookPageDetailModel> GetFacebookPageDetails(long PageId, string pageAccessToken);

        /// <summary>
        /// Gets the instagram all post detail.
        /// </summary>
        /// <param name="tokenModel">The token model.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="InstagramPostDetailModel"/>.</returns>
        Task<List<InstagramPostDetailModel>> GetInstagramAllPostDetail(TokenModel tokenModel);

        /// <summary>
        /// Gets the insta account detail.
        ///   <para>
        ///     Gets the model of type <see cref="TokenModel"/>.
        /// </para>
        /// </summary>
        /// <param name="tokenModel">The token model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="InstagramAccountDetailModel"/>.</returns>
        Task<InstagramAccountDetailModel> GetInstaAccountDetail(TokenModel tokenModel);

        /// <summary>
        /// Gets the instagram account linked with fb page.
        ///   <para>
        ///     Gets the model of type <see cref="TokenModel"/>.
        /// </para>
        /// </summary>
        /// <param name="tokenModel">The token model.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="InstagramAccountLinkedWithFbPageModel"/>.</returns>
        Task<InstagramAccountLinkedWithFbPageModel> GetInstagramAccountLinkedWithFbPage(TokenModel tokenModel);

        /// <summary>
        /// Posts the on social medial channel.
        ///   <para>
        ///     Gets the model of type <see cref="PostOnSocialMediaChannelModel"/>.
        /// </para>
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="ConfirmationPostOnSocialMedialChannelModel"/>.</returns>
        Task<List<ConfirmationPostOnSocialMedialChannelModel>> PostOnSocialMedialChannel(PostOnSocialMediaChannelModel data);

        /// <summary>
        /// Gets the all posts.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="SociaMediaPostModel"/>.</returns>
        Task<List<SociaMediaPostModel>> GetAllPosts();

        /// <summary>
        /// Gets the posts by id.
        ///  <para>
        ///     Gets The <see cref="int"/> postId of post.
        /// </para>
        /// </summary>
        /// <param name="postId">The post id.</param>
        /// <returns>Returns a <see cref="Task"/> of Entity type <see cref="SociaMediaPostModel"/>.</returns>
        Task<SociaMediaPostModel> GetPostsById(int postId);

        /// <summary>
        /// Gets the posts by plate form.
        ///   <para>
        ///     Gets The <see cref="int"/> platformId of plateform.
        /// </para>
        /// </summary>
        /// <param name="platformId">The platform id.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="SociaMediaPostModel"/>.</returns>
        Task<List<SociaMediaPostModel>> GetPostsByPlateForm(int platformId);

        /// <summary>
        /// Gets the all f b page and linked instagram account.
        ///  <para>
        ///     Gets The <see cref="string"/> accessToken of facebook page.
        /// </para>
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <returns>Returns a <see cref="Task"/> of <see cref="List{T}"/> where <c>T</c> is <see cref="AllFBPageAndLinkedInstagramAccountModel"/>.</returns>
        Task<List<AllFBPageAndLinkedInstagramAccountModel>> GetAllFBPageAndLinkedInstagramAccount(string accessToken);
    }
}
