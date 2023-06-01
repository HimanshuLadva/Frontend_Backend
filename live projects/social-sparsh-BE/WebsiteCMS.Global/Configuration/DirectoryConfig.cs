namespace WebsiteCMS.Global.Configurations
{
    public enum AppDirectory
    {
        BotAvatar,
        BotResponse,
        BotImageOrFile,

        WCMSUploadImgs,
        WCMSZip,
        WCMSDefaultImgs,
        WCMSUserImgs,
        WCMSTemporary,

        SCRMFontFamilies,
        SCRMFieldImgs,
        SCRMDownloadImgs,
        SCRMPreTempImgs,
        SCRMLOwQltPreTempImgs,
        SCRMPreLowQualImgs,
        SCRMPubTempImgs,
        SCRMLOwQltPubTempImgs,
        SCRMLowQualImgs,
        SCRMTagImages,
        SCRMCategoryImages,
        SCRMSubCategoryImages,
        SCRMSocialImages,

        SocialMediaImages
    }


    public class DirectoryConfig
    {
        public static string WebRootPath = string.Empty;
        public static string ContentRootPath = string.Empty;
        private static readonly Dictionary<AppDirectory, string> Directories = new();

        private static void InitDirectoryConfigs()
        {
            Directories.Add(AppDirectory.BotAvatar, "Resources/BOT/Avtar");
            Directories.Add(AppDirectory.BotResponse, "Resources/BOT/");
            Directories.Add(AppDirectory.BotImageOrFile, "Resources/BOT/BOTImageORFile/");

            Directories.Add(AppDirectory.WCMSUploadImgs, "Resources/WCMS/Temporary/UploadedImgs");
            Directories.Add(AppDirectory.WCMSZip, "Resources/WCMS/Zip");
            Directories.Add(AppDirectory.WCMSDefaultImgs, Path.Combine(WebRootPath, "WCMS/Images/DefaultImages"));
            Directories.Add(AppDirectory.WCMSUserImgs, Path.Combine(WebRootPath, "WCMS/Images/UserImages"));
            Directories.Add(AppDirectory.WCMSTemporary, "Resources/WCMS/Temporary");

            Directories.Add(AppDirectory.SCRMFontFamilies, "Resources/SCRM/Public/FontFamilies");
            Directories.Add(AppDirectory.SCRMFieldImgs, "Resources/SCRM/Private/Images/FieldImages");
            Directories.Add(AppDirectory.SCRMDownloadImgs, "Resources/SCRM/Private/Images/DownloadImages");
            Directories.Add(AppDirectory.SCRMPreTempImgs, "Resources/SCRM/Private/Images/TemplateImages/PremiumTemplateImages");
            Directories.Add(AppDirectory.SCRMPreLowQualImgs, "Resources/SCRM/Private/Images/TemplateImages/PremiumLowQualityImages");
            Directories.Add(AppDirectory.SCRMPubTempImgs, "Resources/SCRM/Private/Images/TemplateImages/PublicTemplateImages");
            Directories.Add(AppDirectory.SCRMLOwQltPreTempImgs, "Resources/SCRM/Private/Images/TemplateImages/PremiumLowQualityImages");
            Directories.Add(AppDirectory.SCRMLOwQltPubTempImgs, "Resources/SCRM/Private/Images/TemplateImages/PublicLowQualityImages");
            Directories.Add(AppDirectory.SCRMTagImages, "Resources/SCRM/Private/Images/TagImages");
            Directories.Add(AppDirectory.SCRMCategoryImages, "Resources/SCRM/Private/Images/CategoryImages");
            Directories.Add(AppDirectory.SCRMSubCategoryImages, "Resources/SCRM/Private/Images/SubCategoryImages");
            Directories.Add(AppDirectory.SocialMediaImages, "Resources/SCRM/Private/Images/SocialmediaImages/");
            Directories.Add(AppDirectory.SCRMSocialImages, "wwwroot/SCRM/SocialImages");

        }

        public static string Get(AppDirectory appDirectory)
        {
            if (Directories.ContainsKey(appDirectory))
            {
                return Path.Combine(Directory.GetCurrentDirectory(), Directories[appDirectory].Trim());
            }
            else
            {
                throw new Exception($"{appDirectory} doesn't exists");
            }
        }

        public static void Init(string webRootPath, string contentRootPath)
        {
            WebRootPath = webRootPath.Trim();
            ContentRootPath = contentRootPath.Trim();
            InitDirectoryConfigs();
            CreateDirectories();
        }

        private static void CreateDirectories()
        {
            try
            {
                foreach (var item in Directories)
                {
                    if (!Directory.Exists(item.Value))
                    {
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), item.Value.Trim()));
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public static string GetRelativePath(string relativeTo, AppDirectory appDirectory)
        {
            if (Directories.ContainsKey(appDirectory))
            {
                return Path.GetRelativePath(relativeTo, Directories[appDirectory].Trim());
            }
            else
            {
                throw new Exception($"{appDirectory} doesn't exists");
            }
        }
    }
}
