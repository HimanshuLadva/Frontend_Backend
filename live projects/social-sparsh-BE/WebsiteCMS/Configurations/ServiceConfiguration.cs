using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.BLL.Services;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Repositories;

namespace WebsiteCMS.Configurations
{
    public static class ServiceConfiguration
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IAWSImageService, AWSImageService>();

            services.AddScoped<ISCRMAlignRepository, SCRMAlignRepository>();
            services.AddScoped<SCRMICategoryRepository, SCRMCategoryRepository>();
            services.AddScoped<SCRMISubCategoryRepository, SCRMSubCategoryRepository>();
            services.AddScoped<SCRMISocialRepository, SCRMSocialRepository>();
            services.AddScoped<ISCRMCaptionRepository, SCRMCaptionRepository>();
            services.AddScoped<ISocialPlatFormRepository, SocialPlatFormRepository>();

            services.AddScoped<IBOTAvatarRepository, BOTAvatarRepository>();
            services.AddScoped<IBOTComponentRepository, BOTComponentRepository>();
            services.AddScoped<IBotQuestionRepository, BotQuestionRepository>();
            services.AddScoped<IWebHookRepository, WebHookRepository>();
            services.AddScoped<IBotWhatsAppRepository, BotWhatsAppRepository>();
            services.AddScoped<IBOTChatBOTRepository, BOTChatBOTRepository>();
            services.AddScoped<IBOTHistoryRepository, BOTHistoryRepository>();
            services.AddScoped<IBOTVisitorRepository, BOTVisitorRepository>();
            services.AddScoped<IBotWhatsApTempStatusRepository, BotWhatsApTempStatusRepository>();
            services.AddScoped<IBOTWhatsAppTempRegisterIssus, BOTWhatsAppTempRegisterIssus>();
            services.AddScoped<IBOTWhatsAppTemplateRepository, BOTWhatsAppTemplateRepository>();
            services.AddScoped<IBOTWhatsAppChannel, BOTWhatsAppChannel>();
            services.AddScoped<IBOTImageOrFileRepository, BOTImageOrFileRepository>();

            services.AddScoped<IWCMSUserTemplatesRepository, WCMSUserTemplatesRepository>();
            services.AddScoped<IWCMSProductCategoryRepository, WCMSProductCategoryRepository>();
            services.AddScoped<IWCMSProductRepository, WCMSProductRepository>();
            services.AddScoped<IWCMSProductFieldsRepository, WCMSProductFieldsRepository>();
            services.AddScoped<IWCMSTemplatesRepository, WCMSTemplatesRepository>();
            services.AddScoped<IWCMSUserProductFieldRepository, WCMSUserProductFieldRepository>();
            services.AddTransient<IWCMSTemplateRepository, WCMSTemplateRepository>();
            services.AddScoped<IBusinessRepository, BusinessRepository>();
            services.AddScoped<IBusinessCategoryRepository, BusinessCategoryRepository>();
        }

        public static void AddRepositoryServices(this IServiceCollection services)
        {

            services.AddScoped<ISCRMAlignService, SCRMAlignService>();
            services.AddScoped<ISCRMCategoryService, SCRMCategoryService>();
            services.AddScoped<ISCRMSubCategoryServcie, SCRMSubCategoryService>();
            services.AddScoped<ISCRMSocialService, SCRMSocialService>();
            services.AddScoped<ISCRMTemplateService, SCRMTemplateService>();
            services.AddScoped<ISCRMCaptionService, SCRMCaptionService>();

            services.AddScoped<IWCMSTemplatesService, WCMSTemplatesService>();
            services.AddScoped<IBOTAvatarService, BOTAvatarService>();
            services.AddScoped<IBOTComponentService, BOTComponentService>();
            services.AddScoped<IWebHookService, WebHookService>();
            services.AddScoped<IWhatsAppService, WhatsAppService>();
            services.AddScoped<IBOTGetflowService, BOTGetflowService>();
            services.AddScoped<IBOTchatbotService, BOTchatbotService>();
            services.AddScoped<IBOTWebAppChannelService, BOTWebAppChannelService>();
            services.AddScoped<IBOTSessionTracker, BOTSessionTracker>();
            services.AddScoped<IBOTImageOrFileService, BOTImageOrFileService>();

            services.AddScoped<IWCMSUserTemplateInfoService, WCMSUserTemplateInfoService>();
            services.AddScoped<IWCMSProductCategoryService, WCMSProductCategoryService>();
            services.AddScoped<IWCMSProductService, WCMSProductService>();
            services.AddScoped<IWCMSProductFieldService, WCMSProductFieldService>();

            services.AddScoped<IBusinessService, BusinessService>();
            services.AddScoped<IBusinessCategoryService, BusinessCategoryService>();
        }
    }
}