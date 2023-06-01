using AutoMapper;
using NPOI;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.Mapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<WCMSTemplates, WCMSTemplatesModel>();
            CreateMap<WCMSUserTemplates, WCMSUserTemplatesModel>().ReverseMap();
            CreateMap<WCMSFieldsMaster, WCMSFieldsMasterModel>().ReverseMap();
            CreateMap<WCMSMasterType, WCMSMasterTypeModel>().ReverseMap();
            CreateMap<WCMSFieldType, WCMSFieldTypeModel>().ReverseMap();
            CreateMap<WCMSProductCategories, WCMSProductCategoriesModel>().ReverseMap();
            CreateMap<WCMSCategoryWiseProducts, WCMSCategoryWiseProductsModel>().ReverseMap();
            CreateMap<WCMSProductFields, WCMSProductFieldsModel>().ReverseMap();
            CreateMap<WCMSProductFields, WCMSProductFieldsViewModel>().ForMember(y => y.Type, opt => opt.MapFrom(x => x.FieldType.Type));
            CreateMap<WCMSUserProductFields, WCMSUserProductFieldsModel>().ReverseMap();
            CreateMap<WCMSUserProductFields, WCMSUserProductFieldsModel>().ForMember(y => y.FieldName, opt => opt.MapFrom(x => x.ProductFields.Name)).ForMember(y => y.Type, opt => opt.MapFrom(x => x.ProductFields.FieldType.Type));
            CreateMap<WCMSTemplatePageFields, WCMSTemplatePageFieldsModel>().ForMember(y => y.Type, opt => opt.MapFrom(x => x.FieldsMaster.FieldType.Type));

            CreateMap<BOTComponent, BOTComponentModel>().ReverseMap();
            CreateMap<BOTOption, BOTOptionModel>().ReverseMap();
            CreateMap<BOTQuestionLink, BOTQuestionLinkModel>().ReverseMap();
            CreateMap<BOTImageOrFile, BOTImageOrFileModel>().ReverseMap();
            CreateMap<BOTOption, BOTOptionViewModel>().ReverseMap();
            CreateMap<BOTQuestionLink, BOTQuestionLinkViewModel>().ReverseMap();
            CreateMap<BOTQuestion, BOTQuestionModel>().ReverseMap();
            CreateMap<BOTChatBot, BOTChatBotModel>().ReverseMap();
            CreateMap<BOTAvatar, BOTAvatarModel>().ReverseMap();
            CreateMap<BOTHistory, BOTHistoryModel>().ReverseMap();
            CreateMap<BOTVisitor, BOTVisitorModel>().ReverseMap();
            CreateMap<BOTQuestion, BOTQuestionViewModel>().ForMember(y => y.QuestionType, opt => opt.MapFrom(x => x.ComponentType.QuestionType)).ForMember(y => y.InputType, opt => opt.MapFrom(x => x.ComponentType.InputType));
            CreateMap<BOTOption, BOTOptionViewModel>().ReverseMap();
            CreateMap<BOTQuestionLink, BOTQuestionLinkViewModel>().ReverseMap();
            CreateMap<BOTChatBot, BOTChatBotViewModel>();
            CreateMap<BOTHistory, BOTHistoryViewModel>().ForMember(y => y.VisitorUUId, opt => opt.MapFrom(x => x.Visitor.VisitorUUId));
            CreateMap<BOTVisitor, BOTVisitorViewModel>();
            CreateMap<BOTPlatform, BOTPlatformModel>().ReverseMap();
            CreateMap<BOTWhatsAppBusinessData, BOTWhatsAppBusinessDataModel>().ReverseMap();

            CreateMap<SCRMCaptions, SCRMCaptionsModel>().ReverseMap();

            CreateMap<SocialPlateformWisePosts, SocialPlateformWisePostsModel>().ReverseMap();
            CreateMap<SocialPlatforms, SocialPlatformsModel>().ReverseMap();
        }
    }
}
