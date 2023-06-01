using AutoMapper;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Asn1.Tsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.BLL.Services
{
    public class WCMSTemplatesService : IWCMSTemplatesService
    {
        private readonly IWCMSTemplatesRepository _templateRepository;
        private readonly IMapper _mapper;

        public WCMSTemplatesService(IWCMSTemplatesRepository templateRepository, IMapper mapper)
        {
            _templateRepository = templateRepository;
            _mapper = mapper;
        }

        public List<WCMSTemplatesModel> GetAllTemplateAsync(string HostInfo)
        {
            var templates = _templateRepository.GetAllTemplatesAsync().Select(x => new WCMSTemplatesModel()
            {
                Id = x.Id,
                Name = x.Name,
                StoredPathURL = x.StoredPathURL,
                CoverImageURL = !string.IsNullOrEmpty(x.CoverImageURL) ? HostInfo + x.CoverImageURL : string.Empty
            }).ToList();

            return templates;
        }
    }
}
