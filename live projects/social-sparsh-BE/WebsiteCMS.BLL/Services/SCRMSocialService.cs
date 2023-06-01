using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.AuthRequestModel;

namespace WebsiteCMS.BLL.Services
{
    public class SCRMSocialService : ISCRMSocialService
    {
        private IBaseRepository _baseRepository;
        private readonly SCRMISocialRepository _socialRepository;

        public SCRMSocialService(IBaseRepository baseRepository, SCRMISocialRepository socialRepository)
        {

            _baseRepository = baseRepository;
            _socialRepository = socialRepository;
        }

        public async Task<string> FacebookLongLivedToken(string accessToken)
        {
            HttpClient client = new HttpClient();
            var LongLivedToken = await _socialRepository.FacebookLongLivedToken(accessToken);

            return LongLivedToken!;
        }

        public async Task<bool> FBPagesAndTokens(TokenModel tokenModel)
        {
            bool isPagesGettingSuccessfullyOrNot = await _socialRepository.FBPagesAndTokens(tokenModel);
            return isPagesGettingSuccessfullyOrNot;
        }

        public async Task<FacebookPageDetailModel> GetFacebookPageDetails(long pageId, string pageAccessToken)
        {
            FacebookPageDetailModel result = new FacebookPageDetailModel();
            result = await _socialRepository.GetFacebookPageDetails(pageId, pageAccessToken);
            return result;
        }

        public async Task<int> FBPostImage(string PageName, string Caption, IFormFile image)
        {
            int postedImage = await _socialRepository.FBPostImage(PageName, Caption, image);
            return postedImage;
        }

        public async Task<bool> IGPostImage(TokenModel tokenModel, [FromRoute] string imagePath, [FromRoute] string caption)
        {
            bool isImagePostedOrNot = await _socialRepository.IGPostImage(tokenModel, imagePath, caption);
            return isImagePostedOrNot;
        }

        public async Task<List<AllFBPageAndLinkedInstagramAccountModel>> FBPageList()
        {
            List<AllFBPageAndLinkedInstagramAccountModel> result = await _socialRepository.FBPageList();
            return result;
        }

        public async Task<List<InstagramPostDetailModel>> GetInstagramAllPostDetail(TokenModel tokenModel)
        {
            List<InstagramPostDetailModel> allPostDetail = await _socialRepository.GetInstagramAllPostDetail(tokenModel);
            return allPostDetail;
        }

        public async Task<InstagramAccountDetailModel> GetInstaAccountDetail(TokenModel tokenModel)
        {
            InstagramAccountDetailModel accountDetail = await _socialRepository.GetInstaAccountDetail(tokenModel);
            return accountDetail;
        }

        public async Task<InstagramAccountLinkedWithFbPageModel> GetInstagramAccountLinkedWithFbPage(TokenModel tokenModel)
        {
            InstagramAccountLinkedWithFbPageModel pageInfo = await _socialRepository.GetInstagramAccountLinkedWithFbPage(tokenModel);
            return pageInfo;
        }

        public async Task<List<ConfirmationPostOnSocialMedialChannelModel>> PostOnSocialMedialChannel(PostOnSocialMediaChannelModel data)
        {
            List<ConfirmationPostOnSocialMedialChannelModel> postDetail = await _socialRepository.PostOnSocialMedialChannel(data);

            return postDetail;
        }

        public async Task<List<SociaMediaPostModel>> GetAllPosts()
        {
            var Details = await _socialRepository.GetAllPosts();
            return Details;
        }

        public async Task<SociaMediaPostModel> GetPostsById(int PostId)
        {
            var Post = await _socialRepository.GetPostsById(PostId);
            return Post;
        }

        public async Task<List<SociaMediaPostModel>> GetPostsByPlateForm(int platformId)
        {
            var Details = await _socialRepository.GetPostsByPlateForm(platformId);
            return Details;
        }

        public async Task<List<AllFBPageAndLinkedInstagramAccountModel>> GetAllFBPageAndLinkedInstagramAccount(string accessToken)
        {
            var ListOfAccounts = await _socialRepository.GetAllFBPageAndLinkedInstagramAccount(accessToken);
            return ListOfAccounts;
        }
    }
}
