using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;

namespace WebsiteCMS.BLL.Services
{
    public class SCRMTemplateService : ISCRMTemplateService
    {
        private readonly SCRMITemplateRepository _templateRepository;
        private readonly SCRMITemplateLayoutRepository _templateLayoutRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public SCRMTemplateService(SCRMITemplateRepository templateRepository,
                                   SCRMITemplateLayoutRepository templateLayoutRepository,
                                   IHttpContextAccessor contextAccessor)
        {
            _templateRepository = templateRepository;
            _templateLayoutRepository = templateLayoutRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task<SCRMTemplateMetadateAndLayoutModel> TemplateMetadateAndLayoutByIdAsync(int templateId)
        {
            var Request = _contextAccessor.HttpContext.Request;
            var baseURL = Request.Scheme + Uri.SchemeDelimiter + Request.Host.Value + "/";

            var templateMetadata = await _templateRepository.GetTemplateByIdAsync(templateId);
            if (templateMetadata != null)
            {
                templateMetadata.TemplateImageURL = !string.IsNullOrEmpty(templateMetadata.TemplateImageURL) ? baseURL + templateMetadata.TemplateImageURL : string.Empty;
            }
            var templateLayout = await _templateLayoutRepository.GetTemplateLayoutByIdAsync(templateId);

            SCRMTemplateMetadateAndLayoutModel data = new()
            {
                TemplateId = templateId,
                TemplateLayout = templateLayout,
                TemplateMetadata = templateMetadata
            };
            return data;
        }
    }
}
