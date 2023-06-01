using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Permissions;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.DAL.Data.Models
{
    /// <summary>
    ///     A class that is responsible for accessing the database.
    ///     <list type="bullet">
    ///         <item><description>It is responsible for CRUD operations.</description></item>
    ///         <item><description>It is responsible for migrating the database as it is used in <see cref="Migrations.WebsiteCMSDbContextModelSnapshot"/> because of the code first approach.</description></item>
    ///     </list>
    /// </summary>
    public class WebsiteCMSDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WebsiteCMSDbContext"/> class.
        /// </summary>
        /// <param name="options">The <see cref="DbContextOptions{TContext}"/> where <c>TContext</c> is <see cref="WebsiteCMSDbContext"/>.</param>
        public WebsiteCMSDbContext(DbContextOptions<WebsiteCMSDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        ///     A property to access Table - AspNetUserProfile.
        /// </summary>
        public virtual DbSet<AspNetUserProfile> AspNetUserProfile { get; set; } = null!;
        /// <summary>
        ///     A property to access Table - AspNetRefreshTokens.
        /// </summary>
        public virtual DbSet<AspNetRefreshToken> AspNetRefreshTokens { get; set; } = null!;
        /// <summary>
        ///     A property to access Table - tblCountry.
        /// </summary>
        public DbSet<Countries> tblCountry { get; set; }
        /// <summary>
        ///     A property to access Table - tblState.
        /// </summary>
        public DbSet<States> tblState { get; set; }
        /// <summary>
        ///     A property to access Table - tblCity.
        /// </summary>
        public DbSet<City> tblCity { get; set; }


        /// <summary>
        ///     A property to access Table - tblSCRMTag.
        /// </summary>
        public DbSet<SCRMTag> tblSCRMTag { get; set; }
        /// <summary>
        ///     A property to access Table - tblSCRMCategory.
        /// </summary>
        public DbSet<SCRMCategory> tblSCRMCategory { get; set; }
        /// <summary>
        ///     A property to access Table - tblSCRMTemplateField.
        /// </summary>
        public DbSet<SCRMTemplateField> tblSCRMTemplateField { get; set; }
        /// <summary>
        ///     A property to access Table - tblSCRMTemplateFieldType.
        /// </summary>
        public DbSet<SCRMTemplateFieldType> tblSCRMTemplateFieldType { get; set; }
        /// <summary>
        ///     A property to access Table - tblSCRMTemplate.
        /// </summary>
        public DbSet<SCRMTemplate> tblSCRMTemplate { get; set; }
        /// <summary>
        ///     A property to access Table - tblSCRMTemplateTag.
        /// </summary>
        public DbSet<SCRMTemplateTag> tblSCRMTemplateTag { get; set; }
        /// <summary>
        ///     A property to access Table - tblSCRMFontFamily.
        /// </summary>
        public DbSet<SCRMFontFamily> tblSCRMFontFamily { get; set; }
        /// <summary>
        ///     A property to access Table - tblSCRMAlign.
        /// </summary>
        public DbSet<SCRMAlign> tblSCRMAlign { get; set; }
        /// <summary>
        ///     A property to access Table - tblSCRMTemplateFieldText.
        /// </summary>
        public DbSet<SCRMTemplateFieldText> tblSCRMTemplateFieldText { get; set; }
        /// <summary>
        ///     A property to access Table - tblSCRMTemplateFieldImage.
        /// </summary>
        public DbSet<SCRMTemplateFieldImage> tblSCRMTemplateFieldImage { get; set; }
        /// <summary>
        ///     A property to access Table - tblSCRMUserMetaData.
        /// </summary>
        public DbSet<SCRMUserMetaData> tblSCRMUserMetaData { get; set; }
        /// <summary>
        ///     A property to access Table - tblSCRMUserCategoryList.
        /// </summary>
        public DbSet<SCRMUserCategoryList> tblSCRMUserCategoryList { get; set; }
        /// <summary>
        ///     A property to access Table - tblSCRMLanguage.
        /// </summary>
        public DbSet<SCRMLanguage> tblSCRMLanguage { get; set; }
        /// <summary>
        ///     A property to access Table - tblSCRMSubCategory.
        /// </summary>
        public DbSet<SCRMSubCategory> tblSCRMSubCategory { get; set; }
        /// <summary>
        ///     A property to access Table - tblSCRMTemplateSubCategory.
        /// </summary>
        public DbSet<SCRMTemplateSubCategory> tblSCRMTemplateSubCategory { get; set; }
        /// <summary>
        ///     A property to access Table - tblSCRMColor.
        /// </summary>
        public DbSet<SCRMColor> tblSCRMColor { get; set; }
        /// <summary>
        ///     A property to access Table - tblSCRMTemplateCategory.
        /// </summary>
        public DbSet<SCRMTemplateCategory> tblSCRMTemplateCategory { get; set; }
        /// <summary>
        ///     A property to access Table - tblSCRMCaptions.
        /// </summary>
        public DbSet<SCRMCaptions> tblSCRMCaptions { get; set; }

        // Client Onboarding Details

        /// <summary>
        ///     A property to access Table - tblBusinessInfo.
        /// </summary>
        public DbSet<BusinessInfo> tblBusinessInfo { get; set; }
        /// <summary>
        ///     A property to access Table - tblBusinessCategorys.
        /// </summary>
        public DbSet<BusinessCategory> tblBusinessCategorys { get; set; }
        /// <summary>
        ///     A property to access Table - tblBusinessInfoCategorys.
        /// </summary>
        public DbSet<BusinessInfoCategories> tblBusinessInfoCategorys { get; set; }
        /// <summary>
        ///     A property to access Table - tblBusinessContactInfo.
        /// </summary>
        public DbSet<BusinessContactInfo> tblBusinessContactInfo { get; set; }
        /// <summary>
        ///     A property to access Table - tblBusinessPhoneNos.
        /// </summary>
        public DbSet<BusinessPhoneNos> tblBusinessPhoneNos { get; set; }
        /// <summary>
        ///     A property to access Table - tblBusinessLocationInfo.
        /// </summary>
        public DbSet<BusinessLocationInfo> tblBusinessLocationInfo { get; set; }
        /// <summary>
        ///     A property to access Table - tblBusinessServiceArea.
        /// </summary>
        public DbSet<BusinessServiceArea> tblBusinessServiceArea { get; set; }


        //SocialMedia

        /// <summary>
        ///     A property to access Table - tblFacebookPagesTokens.
        /// </summary>
        public DbSet<FacebookPagesTokens> tblFacebookPagesTokens { get; set; }
        /// <summary>
        ///     A property to access Table - tblSociaMediaPost.
        /// </summary>
        public DbSet<SociaMediaPost> tblSociaMediaPost { get; set; }
        /// <summary>
        ///     A property to access Table - tblSocialPlatforms.
        /// </summary>
        public DbSet<SocialPlatforms> tblSocialPlatforms { get; set; }
        /// <summary>
        ///     A property to access Table - tblSocialPlateformWisePosts.
        /// </summary>
        public DbSet<SocialPlateformWisePosts> tblSocialPlateformWisePosts { get; set; }


        //WCMS

        /// <summary>
        ///     A property to access Table - tblWCMSFieldType.
        ///     Has the attributes(columns) of the properties in <see cref="WCMSFieldType"/>
        /// </summary>
        public DbSet<WCMSFieldType> tblWCMSFieldType { get; set; }
        /// <summary>
        ///     A property to access Table - tblWCMSTemplates.
        ///     Has the attributes(columns) of the properties in <see cref="WCMSTemplates"/>
        /// </summary>
        public DbSet<WCMSTemplates> tblWCMSTemplates { get; set; }
        /// <summary>
        ///     A property to access Table - tblWCMSUserTemplatesChild.
        ///     Has the attributes(columns) of the properties in <see cref="WCMSUserTemplatesChild"/>
        /// </summary>
        public DbSet<WCMSUserTemplatesChild> tblWCMSUserTemplatesChild { get; set; }
        /// <summary>
        ///     A property to access Table - tblWCMSTemplatePages.
        ///     Has the attributes(columns) of the properties in <see cref="WCMSTemplatePages"/>
        /// </summary>
        public DbSet<WCMSTemplatePages> tblWCMSTemplatePages { get; set; }
        /// <summary>
        ///     A property to access Table - tblWCMSTemplatePageType.
        ///     Has the attributes(columns) of the properties in <see cref="WCMSTemplatePageType"/>
        /// </summary>
        public DbSet<WCMSTemplatePageType> tblWCMSTemplatePageType { get; set; }
        /// <summary>
        ///     A property to access Table - tblWCMSFieldsMasters.
        ///     Has the attributes(columns) of the properties in <see cref="WCMSFieldsMaster"/>
        /// </summary>
        public DbSet<WCMSFieldsMaster> tblWCMSFieldsMasters { get; set; }
        /// <summary>
        ///     A property to access Table - tblWCMSFieldsMasterChild.
        ///     Has the attributes(columns) of the properties in <see cref="WCMSFieldsMasterChild"/>
        /// </summary>
        public DbSet<WCMSFieldsMasterChild> tblWCMSFieldsMasterChild { get; set; }
        /// <summary>
        ///     A property to access Table - tblWCMSMasterType.
        ///     Has the attributes(columns) of the properties in <see cref="WCMSMasterType"/>
        /// </summary>
        public DbSet<WCMSMasterType> tblWCMSMasterType { get; set; }
        /// <summary>
        ///     A property to access Table - tblWCMSTemplatePageFields.
        ///     Has the attributes(columns) of the properties in <see cref="WCMSTemplatePageFields"/>
        /// </summary>
        public DbSet<WCMSTemplatePageFields> tblWCMSTemplatePageFields { get; set; }
        /// <summary>
        ///     A property to access Table - tblWCMSUserTemplates.
        ///     Has the attributes(columns) of the properties in <see cref="WCMSUserTemplates"/>
        /// </summary>
        public DbSet<WCMSUserTemplates> tblWCMSUserTemplates { get; set; }
        /// <summary>
        ///     A property to access Table - tblWCMSUserTemplateDetails.
        ///     Has the attributes(columns) of the properties in <see cref="WCMSUserTemplateDetails"/>
        /// </summary>
        public DbSet<WCMSUserTemplateDetails> tblWCMSUserTemplateDetails { get; set; }
        /// <summary>
        ///     A property to access Table - tblWCMSUserTemplateDetailsChilds.
        ///     Has the attributes(columns) of the properties in <see cref="WCMSUserTemplateDetailsChilds"/>
        /// </summary>
        public DbSet<WCMSUserTemplateDetailsChilds> tblWCMSUserTemplateDetailsChilds { get; set; }
        /// <summary>
        ///     A property to access Table - tblWCMSCatalog.
        ///     Has the attributes(columns) of the properties in <see cref="WCMSCatalog"/>
        /// </summary>
        public DbSet<WCMSCatalog> tblWCMSCatalog { get; set; }
        /// <summary>
        ///     A property to access Table - tblWCMSProductCategories.
        ///     Has the attributes(columns) of the properties in <see cref="WCMSProductCategories"/>
        /// </summary>
        public DbSet<WCMSProductCategories> tblWCMSProductCategories { get; set; }
        /// <summary>
        ///     A property to access Table - tblWCMSCategoryWiseProducts.
        ///     Has the attributes(columns) of the properties in <see cref="WCMSCategoryWiseProducts"/>
        /// </summary>
        public DbSet<WCMSCategoryWiseProducts> tblWCMSCategoryWiseProducts { get; set; }
        /// <summary>
        ///     A property to access Table - tblWCMSProductFields.
        ///     Has the attributes(columns) of the properties in <see cref="WCMSProductFields"/>
        /// </summary>
        public DbSet<WCMSProductFields> tblWCMSProductFields { get; set; }
        /// <summary>
        ///     A property to access Table - tblWCMSUserProductFields.
        ///     Has the attributes(columns) of the properties in <see cref="WCMSUserProductFields"/>
        /// </summary>
        public DbSet<WCMSUserProductFields> tblWCMSUserProductFields { get; set; }

        //Bot

        /// <summary>
        ///     A property to access Table - tblBOTComponent.
        /// </summary>
        public DbSet<BOTComponent> tblBOTComponent { get; set; }
        /// <summary>
        ///     A property to access Table - tblBOTQuestion.
        /// </summary>
        public DbSet<BOTQuestion> tblBOTQuestion { get; set; }
        /// <summary>
        ///     A property to access Table - tblBOTOption.
        /// </summary>
        public DbSet<BOTOption> tblBOTOption { get; set; }
        /// <summary>
        ///     A property to access Table - tblBOTQuestionLink.
        /// </summary>
        public DbSet<BOTQuestionLink> tblBOTQuestionLink { get; set; }
        /// <summary>
        ///     A property to access Table - tblBOTChatBot.
        /// </summary>
        public DbSet<BOTChatBot> tblBOTChatBot { get; set; }
        /// <summary>
        ///     A property to access Table - tblBOTAvatar.
        /// </summary>
        public DbSet<BOTAvatar> tblBOTAvatar { get; set; }
        /// <summary>
        ///     A property to access Table - tblBOTHistory.
        /// </summary>
        public DbSet<BOTHistory> tblBOTHistory { get; set; }
        /// <summary>
        ///     A property to access Table - tblBOTAPILogs.
        /// </summary>
        public DbSet<BOTAPILogs> tblBOTAPILogs { get; set; }
        /// <summary>
        ///     A property to access Table - tblBOTVisitor.
        /// </summary>
        public DbSet<BOTVisitor> tblBOTVisitor { get; set; }
        /// <summary>
        ///     A property to access Table - tblBOTPlatform.
        /// </summary>
        public DbSet<BOTPlatform> tblBOTPlatform { get; set; }
        /// <summary>
        ///     A property to access Table - tblBOTWhatsAppBusinessData.
        /// </summary>
        public DbSet<BOTWhatsAppBusinessData> tblBOTWhatsAppBusinessData { get; set; }
        /// <summary>
        ///     A property to access Table - tblBOTWhatsAppTemplatesStatus.
        /// </summary>
        public DbSet<BOTWhatsAppTemplatesStatus> tblBOTWhatsAppTemplatesStatus { get; set; }
        /// <summary>
        ///     A property to access Table - tblBOTWhatsAppTemplates.
        /// </summary>
        public DbSet<BOTWhatsAppTemplates> tblBOTWhatsAppTemplates { get; set; }
        /// <summary>
        ///     A property to access Table - tblBOTWhatsApptemplateRegisterIssus.
        /// </summary>
        public DbSet<BOTWhatsAppTemplateRegisterIssue> tblBOTWhatsApptemplateRegisterIssus { get; set; }
        /// <summary>
        ///     A property to access Table - tblBOTWebHookResponse.
        /// </summary>
        public DbSet<BOTWebHookResponse> tblBOTWebHookResponse { get; set; }
        /// <summary>
        ///     A property to access Table - tblBOTImagesOrFile.
        /// </summary>
        public DbSet<BOTImageOrFile> tblBOTImagesOrFile { get; set; }

        /// <inheritdoc />
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var markedAsDeleted = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Deleted);

            foreach (var item in markedAsDeleted)
            {
                if (item.Entity is SCRMBase baseDto)
                {
                    item.State = EntityState.Unchanged;
                    baseDto.IsDeleted = true;
                    baseDto.UpdatedDate = DateTime.Now;
                }
                else if (item.Entity is BotBase baseDt)
                {
                    item.State = EntityState.Unchanged;
                    baseDt.IsDeleted = true;
                }
            }
            return base.SaveChanges();
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRefreshToken>(entity =>
            {
                entity.ToTable("AspNetRefreshToken");
            });

            //modelBuilder.Entity<ApplicationUser>(builder =>
            //{
            //    builder.Property(e => e.UserId).ValueGeneratedOnAdd()
            //        .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
            //});
            modelBuilder.Entity<SocialPlateformWisePosts>().HasOne(x => x.SocialPlatforms).WithMany(x => x.SocialPlateformWisePosts).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<WCMSUserTemplateDetails>().HasOne(x => x.UserTemplate).WithMany(x => x.UserTemplateDetails).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<WCMSUserTemplateDetailsChilds>().HasOne(x => x.UserTemplateDetails).WithMany(x => x.UserTemplateDetailsChilds).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<WCMSFieldsMasterChild>().HasOne(x => x.MasterType).WithMany(x => x.TemplateFieldsMaster).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<FacebookPagesTokens>().HasOne(x => x.SocialPlatforms).WithMany(x => x.FacebookPagesTokens).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BOTHistory>().HasOne(x => x.Question).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BOTVisitor>().HasIndex(x => x.VisitorUUId).IsUnique(true);

            modelBuilder.Entity<States>().HasMany(x => x.Cities).WithOne(x => x.States).OnDelete(DeleteBehavior.NoAction);



            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(SCRMBase).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType);
                    var deletedCheck = Expression.Lambda(Expression.Equal(Expression.Property(parameter, "IsDeleted"), Expression.Constant(false)), parameter);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(deletedCheck);
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}