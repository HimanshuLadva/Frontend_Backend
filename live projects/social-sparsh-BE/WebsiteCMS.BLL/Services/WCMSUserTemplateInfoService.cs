using AutoMapper;
using Microsoft.AspNetCore.Http;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.WCMSRequestModel;

namespace WebsiteCMS.BLL.Services
{
    public class WCMSUserTemplateInfoService : IWCMSUserTemplateInfoService
    {
        private readonly IMapper _mapper;
        private readonly IWCMSUserTemplatesRepository _userTemplateInfoRepository;
        private readonly IBaseRepository _baseRepository;

        public WCMSUserTemplateInfoService(IMapper mapper,
                                           IWCMSUserTemplatesRepository userTemplateInfoRepository,
                                           IBaseRepository baseRepository)
        {
            _mapper = mapper;
            _userTemplateInfoRepository = userTemplateInfoRepository;
            _baseRepository = baseRepository;
        }

        public async Task<WCMSResponse> AddUserTemplateInfoAsync(WCMSUserTemplatesModel model)
        {
            var userTemplateInfo = _mapper.Map<WCMSUserTemplates>(model);
            var infoData = await _userTemplateInfoRepository.AddUserTemplateInfoAsync(userTemplateInfo);
            WCMSResponse response = new();
            try
            {
                response = response.ActionResultData(_mapper.Map<WCMSUserTemplatesModel>(infoData), StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response = response.ExceptionResult(ex);
            }
            return response;

        }
        public async Task<WCMSResponse> GetUserTemplateInfoByIdAsync(int TemplateId)
        {
            var userId = _baseRepository.GetUserId();
            var alignData = await _userTemplateInfoRepository.GetUserTemplateinfoByTemplateAsync(TemplateId, userId);
            WCMSResponse response = new();
            try
            {
                response = response.ActionResultData(_mapper.Map<WCMSUserTemplatesModel>(alignData), StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response = response.ExceptionResult(ex);
            }
            return response;
        }
        public async Task<WCMSResponse> DeleteUserTemplateInfoByIdAsync(int id)
        {
            WCMSResponse response = new();
            try
            {
                var result = await _userTemplateInfoRepository.DeleteUserTemplateInfoByIdAsync(id);
                response = response.ActionResultData(result, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response = response.ExceptionResult(ex);
            }
            return response;
        }
    }
}
