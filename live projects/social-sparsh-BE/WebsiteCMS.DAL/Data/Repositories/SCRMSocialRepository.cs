using AutoMapper;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NPOI.HPSF;
using NPOI.OpenXmlFormats.Dml;
using Org.BouncyCastle.Ocsp;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Database.DBRepository;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.AuthRequestModel;
using WebsiteCMS.Global.Configurations;
using static NPOI.HSSF.Util.HSSFColor;
using static System.Net.WebRequestMethods;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class SCRMSocialRepository : SCRMISocialRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly WebsiteCMSDbContext _websiteCMSDbContext;
        private IBaseRepository _baseRepository;
        private readonly ISocialPlatFormRepository _platFormRepository;
        private readonly IAWSImageService _imageRepository;
        private FacebookSettingsModel _fbSettings;

        public SCRMSocialRepository(UserManager<ApplicationUser> userManager,
                                    WebsiteCMSDbContext webContext,
                                    IBaseRepository baseRepository,
                                    IOptions<FacebookSettingsModel> fbSettings,
                                    ISocialPlatFormRepository platFormRepository,
                                    IAWSImageService imageRepository)
        {
            _userManager = userManager;
            _websiteCMSDbContext = webContext;
            _baseRepository = baseRepository;
            _platFormRepository = platFormRepository;
            _imageRepository = imageRepository;
            _fbSettings = fbSettings.Value;
        }



        public async Task<string> FacebookLongLivedToken(string accessToken)
        {
            HttpClient client = new HttpClient();
            var LongLivedToken = await client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id={_fbSettings.AppId}&client_secret={_fbSettings.AppSecret}&fb_exchange_token={accessToken}");

            //var tokenInfo = JsonConvert.DeserializeObject<FBLongLivedTokenModel>(LongLivedToken);

            return LongLivedToken!;
        }
        public async Task<bool> FBPagesAndTokens(TokenModel tokenModel)
        {
            try
            {
                AuthResponse response = new AuthResponse();
                HttpClient client = new HttpClient();
                var userInfoResponse = await client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,first_name,last_name,middle_name,name,name_format,email&access_token={tokenModel.accessToken}");
                var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);
                var pageList = await client.GetStringAsync($"https://graph.facebook.com/{userInfo.id}/accounts?fields=name,access_token&access_token={tokenModel.accessToken}");
                var pageInfo = JsonConvert.DeserializeObject<FacebookPagesTokensModel>(pageList);

                var user = _baseRepository.GetUserId();

                foreach (var Page in pageInfo.data)
                {
                    var IsTokenExsits = _websiteCMSDbContext.tblFacebookPagesTokens.Where(x => x.PageId == Convert.ToInt64(Page.Id)).FirstOrDefault();
                    if (IsTokenExsits == null)
                    {
                        var LongLiveTokenDetails = await FacebookLongLivedToken(Page!.access_token);

                        var LongLiveTokenInfo = JsonConvert.DeserializeObject<FacebookLongLivedTokenModel>(LongLiveTokenDetails);


                        var PlateForms = _platFormRepository.GetAllSocialPlateFormAsync().ToList();
                        var pageToken = new FacebookPagesTokens()
                        {
                            UserId = user,
                            SocialId = Convert.ToInt64(userInfo.id),
                            PageId = Convert.ToInt64(Page.Id),
                            PageName = Page.name.ToLower(),
                            Access_token = LongLiveTokenInfo.access_token,
                            CategoryList = new List<SCRMUserCategoryList>(),
                            SocialPlatformsId = PlateForms.Where(x => x.Name.ToLower() == "FACEBOOK".ToLower()).FirstOrDefault()!.Id,

                        };
                        var pageDetails = await GetFacebookPageDetails(pageToken.PageId, pageToken.Access_token);
                        if (pageDetails != null)
                        {
                            pageToken.PictureUrl = pageDetails!.picture!.data!.url!;
                            pageToken.Cover = pageDetails!.cover != null ? pageDetails!.cover!.source : string.Empty;
                            pageToken.Name = pageDetails.name;
                            pageToken.About = pageDetails.about;
                            pageToken.Phone = pageDetails.phone;
                            pageToken.Bio = pageDetails.bio;
                            pageToken.Birthday = pageDetails.birthday;
                            pageToken.Likes = pageDetails.likes;
                            pageToken.ConnectedInstagramAccount = pageDetails.connected_instagram_account;
                            pageToken.WhatsappNumber = pageDetails.whatsapp_number;
                            pageToken.SingleLineAddress = pageDetails.single_line_address;
                            pageToken.Category = pageDetails.category;
                            pageToken.FollowersCount = pageDetails.followers_count;
                            pageToken.ContactAddress = pageDetails.contact_address;
                            foreach (var item in pageDetails.category_list)
                            {
                                pageToken.CategoryList.Add(new SCRMUserCategoryList()
                                {
                                    CategoryId = item.id,
                                    Name = item.name,

                                });
                            }
                        }

                        await _websiteCMSDbContext.tblFacebookPagesTokens.AddAsync(pageToken);
                    }
                }
                await _websiteCMSDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }

        public async Task<FacebookPageDetailModel> GetFacebookPageDetails(long pageId, string pageAccessToken)
        {
            HttpClient client = new HttpClient();
            var strPageDetails = await client.GetStringAsync($"https://graph.facebook.com/v15.0/{pageId}/?fields=picture,cover,name,about,bio,birthday,category,category_list,contact_address,followers_count,likes,phone,whatsapp_number,connected_instagram_account,single_line_address&access_token={pageAccessToken}");

            //var pageInfo = JsonConvert.DeserializeObject<FacebookPageDetailModel>(strPageDetails);
            var pageInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<FacebookPageDetailModel>(strPageDetails);
            ICollection<SCRMUserCategoryListModel> CategoryList = pageInfo.category_list;
            var result = new FacebookPageDetailModel()
            {
                picture = pageInfo!.picture,
                cover = pageInfo!.cover,
                name = pageInfo.name,
                about = pageInfo.about,
                phone = pageInfo.phone,
                bio = pageInfo.bio,
                birthday = pageInfo.birthday,
                likes = pageInfo.likes,
                connected_instagram_account = pageInfo.connected_instagram_account,
                whatsapp_number = pageInfo.whatsapp_number,
                single_line_address = pageInfo.single_line_address,
                category = pageInfo.category,
                followers_count = pageInfo.followers_count,
                contact_address = pageInfo.contact_address,
                category_list = new List<SCRMUserCategoryListModel>(),
            };

            foreach (var category in CategoryList)
            {
                result.category_list.Add(new SCRMUserCategoryListModel
                {
                    id = category.id,
                    name = category.name,
                });
            }

            return result;
        }

        public async Task<int> FBPostImage(string PageName, string Caption, IFormFile image)
        {
            try
            {
                var applicationUser = await _userManager.GetLoginsAsync(_baseRepository.GetUser());
                var providerKey = "";
                var userId = _baseRepository.GetUserId();

                foreach (var item in applicationUser)
                {
                    if (item.LoginProvider.Equals("FACEBOOK", StringComparison.OrdinalIgnoreCase))
                    {
                        providerKey = item.ProviderKey;
                    }
                }
                if (providerKey != "")
                {
                    var filename = Path.GetFileName(image.FileName);
                    var path = Path.Combine(Path.Combine(_imageRepository.GetAWSDirectory(AWSDirectory.Social), filename));
                    using (Stream fileStream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                    var page = _websiteCMSDbContext.tblFacebookPagesTokens.Include(x => x.SocialPlatforms).Where(x => x.PageName == PageName.ToLower() && x.SocialId == Convert.ToInt64(providerKey) && x.SocialPlatforms!.Name == "FACEBOOK").FirstOrDefault();

                    var client = new RestClient($"https://graph.facebook.com/{page!.PageId}/photos?url={Path.Combine(_imageRepository.GetImageBaseUrl(), path)}&access_token={page.Access_token}");
                    var request = new RestRequest();
                    request.Method = Method.Post;
                    RestResponse resp = client.Execute(request);

                    var APIResp = JsonConvert.DeserializeObject<SCRMSocialModel>(resp.Content!);
                    var postURL = "https://www.facebook.com/" + APIResp!.post_id;
                    Console.WriteLine(resp.Content);

                    var data = new SociaMediaPost()
                    {
                        UserId = userId,
                        PostImage = Path.Combine(_imageRepository.GetImageBaseUrl(), path),
                        PostDate = DateTime.UtcNow,
                        Caption = Caption,
                    };

                    var PlateFormWisePost = new List<SocialPlateformWisePosts>()
                    {
                        new SocialPlateformWisePosts()
                        {
                            PostId = data.Id,
                            PageId = page.Id,
                            Plateformid = page.SocialPlatformsId,
                        }
                    };

                    var Post = InsertPagePostDetails(data, PlateFormWisePost);

                    return Post.Id;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<AllFBPageAndLinkedInstagramAccountModel>> FBPageList()
        {
            try
            {
                var userId = _baseRepository.GetUserId();

                List<AllFBPageAndLinkedInstagramAccountModel> tokensModel = new();

                tokensModel = await _websiteCMSDbContext.tblFacebookPagesTokens.Where(x => x.UserId == userId).Select(y => new AllFBPageAndLinkedInstagramAccountModel()
                {
                    Id = y.Id,
                    UserId = y.UserId,
                    FacebookId = y.SocialId,
                    PageId = y.PageId,
                    PageName = y.PageName,
                    Access_token = y.Access_token,
                    PictureUrl = y.PictureUrl,
                    Cover = y.Cover,
                    Name = y.Name,
                    About = y.About,
                    Bio = y.Bio,
                    Birthday = y.Birthday,
                    Category = y.Category,
                    ContactAddress = y.ContactAddress,
                    FollowersCount = y.FollowersCount,
                    Likes = y.Likes,
                    Phone = y.Phone,
                    WhatsappNumber = y.WhatsappNumber,
                    ConnectedInstagramAccount = y.ConnectedInstagramAccount,
                    SingleLineAddress = y.SingleLineAddress,
                }).ToListAsync();

                return tokensModel;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }
        public async Task<bool> IGPostImage(TokenModel tokenModel, string imagePath, string caption)
        {
            try
            {
                HttpClient client = new HttpClient();
                var strMediaId = await client.GetStringAsync($"https://graph.facebook.com/v15.0/{tokenModel.userId}/media?access_token={tokenModel.accessToken}&image_url={imagePath}&caption={caption}");
                var mediaId = JsonConvert.DeserializeObject<InstagramPostModel>(strMediaId);

                if (mediaId != null)
                {
                    var strPostId = await client.GetStringAsync($"https://graph.facebook.com/v15.0/{tokenModel.userId}/media_publish?access_token={tokenModel.accessToken}&creation_id={mediaId}");
                    var postId = JsonConvert.DeserializeObject<InstagramPostModel>(strPostId);

                    if (postId != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
            return false;
        }

        public async Task<List<InstagramPostDetailModel>> GetInstagramAllPostDetail(TokenModel tokenModel)
        {
            HttpClient httpClient = new HttpClient();
            var strPageDetail = await httpClient.GetStringAsync($"https://graph.instagram.com/{tokenModel.userId}?fields=media,name,username&access_token={tokenModel.accessToken}");

            var pageInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<InstagramModel>(strPageDetail);

            List<InstagramPostDetailModel> allPostDetail = new List<InstagramPostDetailModel>();

            foreach (var item in pageInfo!.data)
            {
                var strPostDetail = await httpClient.GetStringAsync($"https://graph.instagram.com/{item.id}?fields=id,media_type,permalink,media_url,username,caption,timestamp&access_token={tokenModel.accessToken}");

                var postDetail = JsonConvert.DeserializeObject<InstagramPostDetailModel>(strPostDetail);
                allPostDetail.Add(postDetail!);

            }
            return allPostDetail;
        }
        public async Task<InstagramAccountDetailModel> GetInstaAccountDetail(TokenModel tokenModel)
        {
            HttpClient httpClient = new HttpClient();
            var strPageDetail = await httpClient.GetStringAsync($"https://graph.facebook.com/v15.0/{tokenModel.userId}?fields=biography%2Cfollowers_count%2Cfollows_count%2Cmedia_count%2Cname%2Cprofile_picture_url%2Cusername%2Cwebsite&access_token={tokenModel.accessToken}");

            var pageInfo = JsonConvert.DeserializeObject<InstagramAccountDetailModel>(strPageDetail);

            return pageInfo;
        }
        public async Task<InstagramAccountLinkedWithFbPageModel> GetInstagramAccountLinkedWithFbPage(TokenModel tokenModel)
        {
            HttpClient httpClient = new HttpClient();
            var strPageDetail = await httpClient.GetStringAsync($"https://graph.facebook.com/v15.0/{tokenModel.userId}?fields=name,instagram_business_account&access_token={tokenModel.accessToken}");


            var pageInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<InstagramAccountLinkedWithFbPageModel>(strPageDetail);

            return pageInfo;
        }
        public async Task<bool> PostOnFaceBook(string id, string imagePath)
        {
            try
            {
                var applicationUser = await _userManager.GetLoginsAsync(_baseRepository.GetUser());
                var providerKey = "";

                foreach (var item in applicationUser)
                {
                    if (item.LoginProvider.Equals("FACEBOOK", StringComparison.OrdinalIgnoreCase))
                    {
                        providerKey = item.ProviderKey;
                    }
                }
                if (providerKey != "")
                {
                    var page = _websiteCMSDbContext.tblFacebookPagesTokens.Where(x => x.PageId == Convert.ToInt64(id) && x.SocialId == Convert.ToInt64(providerKey)).FirstOrDefault();

                    var client = new RestClient($"https://graph.facebook.com/{page.PageId}/photos?url={imagePath}&access_token={page.Access_token}");
                    var request = new RestRequest();
                    request.Method = Method.Post;
                    RestResponse resp = client.Execute(request);
                    Console.WriteLine(resp.Content);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }
        public async Task<bool> PostOnInstaGram(string id, string imagePath, string caption)
        {
            try
            {
                var page = _websiteCMSDbContext.tblFacebookPagesTokens.Where(x => x.PageId == Convert.ToInt64(id)).FirstOrDefault();
                HttpClient client = new HttpClient();
                var strMediaId = await client.GetStringAsync($"https://graph.facebook.com/v15.0/{page!.PageId}/media?access_token={page.Access_token}&image_url={imagePath}&caption={caption}");
                var mediaId = JsonConvert.DeserializeObject<InstagramPostModel>(strMediaId);

                if (mediaId != null)
                {
                    var strPostId = await client.GetStringAsync($"https://graph.facebook.com/v15.0/{page!.PageId}/media_publish?access_token={page.Access_token}&creation_id={mediaId}");
                    var postId = JsonConvert.DeserializeObject<InstagramPostModel>(strPostId);

                    if (postId != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
            return false;
        }
        public async Task<List<ConfirmationPostOnSocialMedialChannelModel>> PostOnSocialMedialChannel(PostOnSocialMediaChannelModel data)
        {
            bool isPosted;
            string imageUrl = string.Empty;
            var PlateForms = _platFormRepository.GetAllSocialPlateFormAsync().ToList();
            string PlateForm = string.Empty;
            var userId = _baseRepository.GetUserId();
            FacebookPagesTokens facebookPagesToken = new();

            List<ConfirmationPostOnSocialMedialChannelModel> postDetail = new ();

            await using var memoryStream = new MemoryStream();
            await data.CustomImage!.CopyToAsync(memoryStream);
            var imgName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(data.CustomImage.FileName));
            string awsUrl = await _imageRepository.UploadFileAsync(memoryStream, imgName, AWSDirectory.Social);
            imageUrl = _baseRepository.GetImageBaseUrl() + awsUrl;

            SociaMediaPost socialMediaPost = new();
            socialMediaPost.PostImage = imageUrl;

            List<SocialPlateformWisePosts> posts = new();

            foreach (var item in data!.PlateformId!)
            {
                var facebookDetail = CheckPlateFormId(item);
                if (facebookDetail != null)
                {
                    facebookPagesToken = _websiteCMSDbContext!.tblFacebookPagesTokens.Where(x => x.PageId == Convert.ToInt64(item))!.FirstOrDefaultAsync()!.Result!;

                    if (data.CustomImage != null)
                    {
                        isPosted = await PostOnFaceBook(item, imageUrl);
                        posts.Add(new SocialPlateformWisePosts()
                        {
                            PageId = facebookPagesToken.Id,
                            Plateformid = facebookDetail.SocialPlatformsId
                        });
                    }
                    else
                    {
                        var template = await _websiteCMSDbContext.tblSCRMTemplate.FindAsync(Convert.ToInt32(data.templateId));
                        imageUrl = _baseRepository.GetImageBaseUrl() + template!.TemplateImageURL;
                        isPosted = await PostOnFaceBook(item, imageUrl);
                        posts.Add(new SocialPlateformWisePosts()
                        {
                            PageId = facebookPagesToken.Id,
                            Plateformid = facebookDetail.SocialPlatformsId
                        });
                    }
                    PlateForm = "FACEBOOK";
                }
                else
                {
                    if (data.CustomImage != null)
                    {
                        isPosted = await PostOnInstaGram(item, imageUrl, data!.Caption!);
                        posts.Add(new SocialPlateformWisePosts()
                        {
                            Plateformid = PlateForms.Where(x => x.Name == "INSTAGRAM").Select(x => x.Id).SingleOrDefault()
                        });
                    }
                    else
                    {
                        var template = await _websiteCMSDbContext.tblSCRMTemplate.FindAsync(Convert.ToInt32(data.templateId));
                        imageUrl = _baseRepository.GetImageBaseUrl() + template!.TemplateImageURL;
                        isPosted = await PostOnInstaGram(item, imageUrl, data!.Caption!);
                        posts.Add(new SocialPlateformWisePosts()
                        {
                            Plateformid = PlateForms.Where(x => x.Name == "INSTAGRAM").Select(x => x.Id).SingleOrDefault()
                        });
                    }
                    PlateForm = "INSTAGRAM";
                }

                postDetail.Add(new ConfirmationPostOnSocialMedialChannelModel()
                {
                    Id = item,
                    PlatForm = PlateForm,
                    isImagePosted = isPosted,
                    ProfileName = facebookDetail!.PageName != null ? facebookDetail!.PageName : null,
                });
                socialMediaPost.PostDate = DateTime.Now;
            }

            socialMediaPost.UserId = userId;
            socialMediaPost.Caption = data.Caption!;

            var posted = await InsertPagePostDetails(socialMediaPost, posts);
            return postDetail;
        }

        public async Task<List<SociaMediaPostModel>> GetAllPosts()
        {
            var userid = _baseRepository.GetUserId();

            List<SociaMediaPost> GetPosts = await _websiteCMSDbContext.tblSociaMediaPost.Where(x => x.UserId == userid).Include(x => x.SocialPlateformWisePosts)!.ThenInclude(x => x.FacebookPagesTokens).Include(x => x.SocialPlateformWisePosts)!.ThenInclude(x => x.SocialPlatforms).ToListAsync();

            List<SociaMediaPostModel> posts = GetPosts.Select(x => new SociaMediaPostModel()
            {
                Id = x.Id,
                UserId = x.UserId,
                PostImage = x.PostImage,
                Caption = x.Caption,
                PostDate = x.PostDate,
                SocialPlateformWisePosts = x.SocialPlateformWisePosts!.Select(y => new SocialPlateformWisePostsModel()
                {
                    Id = y.Id,
                    PostId = y.PostId,
                    PageId = y.PageId,
                    PlateformType = y.SocialPlatforms!.Name,
                    FacebookPagesTokens = new AllFBPageAndLinkedInstagramAccountModel()
                    {
                        Id = y.FacebookPagesTokens!.Id,
                        UserId = y.FacebookPagesTokens!.UserId,
                        FacebookId = y.FacebookPagesTokens!.SocialId,
                        PageId = y.FacebookPagesTokens!.PageId,
                        PageName = y.FacebookPagesTokens!.PageName,
                        Access_token = y.FacebookPagesTokens!.Access_token,
                        PictureUrl = y.FacebookPagesTokens!.PictureUrl,
                        Cover = y.FacebookPagesTokens!.Cover,
                        Name = y.FacebookPagesTokens!.Name,
                        About = y.FacebookPagesTokens!.About,
                        Bio = y.FacebookPagesTokens!.Bio,
                        Birthday = y.FacebookPagesTokens!.Birthday,
                        Category = y.FacebookPagesTokens!.Category,
                        ContactAddress = y.FacebookPagesTokens!.ContactAddress,
                        FollowersCount = y.FacebookPagesTokens!.FollowersCount,
                        Likes = y.FacebookPagesTokens!.Likes,
                        Phone = y.FacebookPagesTokens!.Phone,
                        WhatsappNumber = y.FacebookPagesTokens!.WhatsappNumber,
                        ConnectedInstagramAccount = y.FacebookPagesTokens!.ConnectedInstagramAccount,
                        SingleLineAddress = y.FacebookPagesTokens!.SingleLineAddress,
                    }
                }).ToList()
            }).ToList();
            return posts;
        }
        public async Task<SociaMediaPostModel> GetPostsById(int postId)
        {
            var userid = _baseRepository.GetUserId();

            var Post = await _websiteCMSDbContext.tblSociaMediaPost.Where(x => x.Id == postId).Include(x => x.SocialPlateformWisePosts)!.ThenInclude(x => x.FacebookPagesTokens).Include(x => x.SocialPlateformWisePosts)!.ThenInclude(x => x.SocialPlatforms).Select(x => new SociaMediaPostModel()
            {
                Id = x.Id,
                UserId = x.UserId,
                PostImage = x.PostImage,
                Caption = x.Caption,
                PostDate = x.PostDate,
                SocialPlateformWisePosts = x.SocialPlateformWisePosts!.Select(y => new SocialPlateformWisePostsModel()
                {
                    Id = y.Id,
                    PostId = y.PostId,
                    PageId = y.PageId,
                    PlateformType = y.SocialPlatforms!.Name,
                    FacebookPagesTokens = new AllFBPageAndLinkedInstagramAccountModel()
                    {
                        Id = y.Id,
                        UserId = y.FacebookPagesTokens!.UserId,
                        FacebookId = y.FacebookPagesTokens!.SocialId,
                        PageId = y.FacebookPagesTokens!.PageId,
                        PageName = y.FacebookPagesTokens!.PageName,
                        Access_token = y.FacebookPagesTokens!.Access_token,
                        PictureUrl = y.FacebookPagesTokens!.PictureUrl,
                        Cover = y.FacebookPagesTokens!.Cover,
                        Name = y.FacebookPagesTokens!.Name,
                        About = y.FacebookPagesTokens!.About,
                        Bio = y.FacebookPagesTokens!.Bio,
                        Birthday = y.FacebookPagesTokens!.Birthday,
                        Category = y.FacebookPagesTokens!.Category,
                        ContactAddress = y.FacebookPagesTokens!.ContactAddress,
                        FollowersCount = y.FacebookPagesTokens!.FollowersCount,
                        Likes = y.FacebookPagesTokens!.Likes,
                        Phone = y.FacebookPagesTokens!.Phone,
                        WhatsappNumber = y.FacebookPagesTokens!.WhatsappNumber,
                        ConnectedInstagramAccount = y.FacebookPagesTokens!.ConnectedInstagramAccount,
                        SingleLineAddress = y.FacebookPagesTokens!.SingleLineAddress,
                    }
                }).ToList()
            }).SingleOrDefaultAsync();

            return Post;
        }
        public async Task<List<SociaMediaPostModel>> GetPostsByPlateForm(int platformId)
        {
            var userid = _baseRepository.GetUserId();

            List<SociaMediaPost> GetPosts = await _websiteCMSDbContext.tblSociaMediaPost.Where(x => x.UserId == userid).ToListAsync();

            List<SociaMediaPostModel> posts = GetPosts.Select(x => new SociaMediaPostModel()
            {
                Id = x.Id,
                UserId = x.UserId,
                PostDate = x.PostDate,
            }).ToList();

            return posts;
        }
        public FacebookPagesTokens CheckPlateFormId(string Id)
        {
            var isInFBOrIG = _websiteCMSDbContext.tblFacebookPagesTokens.Where(x => x.PageId == Convert.ToInt64(Id)).FirstOrDefaultAsync().Result;

            if (isInFBOrIG != null)
            {
                return isInFBOrIG!;
            }
            return null;
        }
        public async Task<List<AllFBPageAndLinkedInstagramAccountModel>> GetAllFBPageAndLinkedInstagramAccount(string accessToken)
        {
            HttpClient httpClient = new HttpClient();
            var ListOfAccounts = new List<AllFBPageAndLinkedInstagramAccountModel>();

            var strUserDetail = await httpClient.GetStringAsync($"https://graph.facebook.com/v15.0/me?fields=id&access_token={accessToken}");
            var userDetail = JsonConvert.DeserializeObject<FacebookModel>(strUserDetail);

            var FBPages = await _websiteCMSDbContext.tblFacebookPagesTokens.Where(x => x.SocialId == userDetail.Id).ToListAsync();

            foreach (var item in FBPages)
            {
                var data = await GetInstagramAccountLinkedWithFbPage(new TokenModel() { userId = item.PageId.ToString(), accessToken = item.Access_token!, RememberMe = "true" });
                var result = new AllFBPageAndLinkedInstagramAccountModel()
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    FacebookId = item.SocialId,
                    PageId = item.PageId,
                    PageName = item.PageName,
                    Access_token = item.Access_token,
                    PictureUrl = item!.PictureUrl,
                    Cover = item!.Cover,
                    Name = item!.Name,
                    About = item!.About,
                    Phone = item!.Phone,
                    Bio = item.Bio,
                    Birthday = item.Birthday,
                    Likes = Convert.ToInt32(item.Likes),
                    ConnectedInstagramAccount = item.ConnectedInstagramAccount,
                    WhatsappNumber = item.WhatsappNumber,
                    SingleLineAddress = item!.SingleLineAddress,
                    Category = item!.Category,
                    FollowersCount = Convert.ToInt32(item.FollowersCount),
                    ContactAddress = item!.ContactAddress,
                };
                ListOfAccounts.Add(result);
            }
            return ListOfAccounts;
        }

        public async Task<SociaMediaPost> InsertPagePostDetails(SociaMediaPost data, List<SocialPlateformWisePosts> plateformWisePosts)
        {
            try
            {
                var post = new SociaMediaPost()
                {
                    UserId = data.UserId,
                    PostDate = data.PostDate,
                    PostImage = data.PostImage,
                    Caption = data.Caption
                };
                await _websiteCMSDbContext.tblSociaMediaPost.AddAsync(post);
                await _websiteCMSDbContext.SaveChangesAsync();
                foreach (var item in plateformWisePosts)
                {
                    item.PostId = post.Id;
                    await _websiteCMSDbContext.tblSocialPlateformWisePosts.AddAsync(item);
                }
                await _websiteCMSDbContext.SaveChangesAsync();

                return post;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

