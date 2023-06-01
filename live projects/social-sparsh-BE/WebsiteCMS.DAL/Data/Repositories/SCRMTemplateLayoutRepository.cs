using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using WebsiteCMS.Global.Configurations;
using X.PagedList;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class SCRMTemplateLayoutRepository : SCRMITemplateLayoutRepository
    {
        private readonly WebsiteCMSDbContext _context;
        private IBaseRepository _baseRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IAWSImageService _imageService;

        public SCRMTemplateLayoutRepository(WebsiteCMSDbContext context, IBaseRepository baseRepository, IHttpContextAccessor contextAccessor, IAWSImageService imageService)
        {
            _context = context;
            _baseRepository = baseRepository;
            _contextAccessor = contextAccessor;
            _imageService = imageService;
        }

        public async Task<IPagedList<SCRMTemplateLayoutModel>> GetAllTemplateLayoutAsync(SCRMRequestParams requestParams)
        {
            var textFieldTBL = _context.tblSCRMTemplateFieldText;
            var imageFieldTBL = _context.tblSCRMTemplateFieldImage;
            var records = new List<SCRMTemplateLayoutModel>();
            records = await _context.tblSCRMTemplate
               .Select(s => new SCRMTemplateLayoutModel()
               {
                   Id = s.Id,
                   TemplateImageURL = s.IsFree ? _imageService.GetImageBaseUrl() + s.PremiumTemplateImageURL : _imageService.GetImageBaseUrl() + s.PublicTemplateImageURL,

                   TextFields = textFieldTBL.Where(t => t.TemplateId == s.Id)
                   .Select(x => new SCRMTemplateFieldTextModel()
                   {
                       Id = x.Id,
                       TemplateId = s.Id,
                       TemplateName = x.Template.Name,
                       TemplateFieldId = x.TemplateFieldId,
                       TemplateFieldName = x.TemplateField.Name,
                       FontFamilyId = x.FontFamilyId,
                       Value = x.TemplateField.Value,
                       FontFamilyName = x.FontFamily.Name,
                       IsDisplay = x.IsDisplay,
                       X = x.X,
                       Y = x.Y,
                       AlignId = x.AlignId,
                       Align = x.Align.Name,
                       Size = x.Size,
                       Color = x.Color
                   }).ToList(),

                   ImageFields = imageFieldTBL.Where(t => t.TemplateId == s.Id)
                   .Select(x => new SCRMTemplateFieldImageModel()
                   {
                       Id = x.Id,
                       TemplateId = s.Id,
                       TemplateName = x.Template.Name,
                       TemplateFieldId = x.TemplateFieldId,
                       TemplateFieldName = x.TemplateField.Name,
                       Value = x.TemplateField.Value,
                       IsDisplay = x.IsDisplay,
                       X = x.X,
                       Y = x.Y,
                       Width = x.Width,
                       Height = x.Height
                   }).ToList()
               }).ToListAsync();

            requestParams.recordCount = records.Count;

            var data = await SortResult(records, requestParams);
            return data;
        }

        public async Task<SCRMTemplateLayoutModel> GetTemplateLayoutByIdAsync(int templateId)
        {
            var req = _contextAccessor.HttpContext.Request;

            var templateFileds = await _context.tblSCRMTemplateField
                    .Include(x => x.TemplateFieldType)
                    .Where(x => x.IsActive == true)
                    .ToListAsync();

            var userBrandInfo = await _context.tblSCRMUserMetaData
                    .Include(x => x.TemplateField)
                    .ThenInclude(x => x.TemplateFieldType)
                    .Where(x => x.ApplicationUserId == _baseRepository.GetUserId())
                    .ToListAsync();

            var template = await _context.tblSCRMTemplate.Where(x => x.Id == templateId).FirstOrDefaultAsync();

            var textFieldTBL = await _context.tblSCRMTemplateFieldText
                    .Include(x => x.TemplateField)
                    .Include(x => x.FontFamily)
                    .Include(x => x.Align)
                    .Where(x => x.TemplateId == templateId).ToListAsync();

            var imageFieldTBL = await _context.tblSCRMTemplateFieldImage
                    .Include(x => x.TemplateField)
                    .Where(x => x.TemplateId == templateId).ToListAsync();

            SCRMTemplateLayoutModel obj = new SCRMTemplateLayoutModel();
            List<SCRMTemplateFieldTextModel> lstTextField = new List<SCRMTemplateFieldTextModel>();
            List<SCRMTemplateFieldImageModel> lstImageField = new List<SCRMTemplateFieldImageModel>();

            obj.Id = templateId;
            obj.TemplateImageURL = template!.IsFree ? DirectoryConfig.Get(AppDirectory.SCRMPreTempImgs) + template.TemplateImageURL : DirectoryConfig.Get(AppDirectory.SCRMPubTempImgs) + template.TemplateImageURL;
            if (!string.IsNullOrEmpty(obj.TemplateImageURL))
            {
                obj.TemplateImageURL = req.Scheme + Uri.SchemeDelimiter + req.Host.Value + "/" + obj.TemplateImageURL;
            }

            foreach (var item in textFieldTBL)
            {
                SCRMTemplateFieldTextModel textField = new SCRMTemplateFieldTextModel();
                var tField = templateFileds.Where(x => x.Id == item.TemplateFieldId).FirstOrDefault();

                if (tField != null)
                {
                    var brandInfo = userBrandInfo.Where(x => x.TemplateField == item.TemplateField).FirstOrDefault();

                    textField.Id = item.Id;
                    textField.TemplateId = templateId;
                    textField.TemplateName = template.Name;
                    textField.TemplateFieldId = item.TemplateFieldId;
                    textField.TemplateFieldName = item.TemplateField.Name;
                    textField.FontFamilyId = item.FontFamilyId;
                    textField.Value = brandInfo != null ? brandInfo.Value : string.Empty;
                    textField.FontFamilyName = item.FontFamily.Name;
                    textField.IsDisplay = item.IsDisplay;
                    textField.X = item.X;
                    textField.Y = item.Y;
                    textField.AlignId = item.AlignId;
                    textField.Align = item.Align.Name;
                    textField.Size = item.Size;
                    textField.Color = item.Color;

                    lstTextField.Add(textField);
                }

            }

            obj.TextFields = lstTextField;

            foreach (var itemImage in imageFieldTBL)
            {
                SCRMTemplateFieldImageModel imageField = new SCRMTemplateFieldImageModel();

                var tField = templateFileds.Where(x => x.Id == itemImage.TemplateFieldId).FirstOrDefault();
                if (tField != null)
                {
                    var brandInfo = userBrandInfo.Where(x => x.TemplateField == itemImage.TemplateField).FirstOrDefault();

                    imageField.Id = itemImage.Id;
                    imageField.TemplateId = templateId;
                    imageField.TemplateName = template.Name;
                    imageField.TemplateFieldId = itemImage.TemplateFieldId;
                    imageField.TemplateFieldName = itemImage.TemplateField.Name;
                    imageField.Value = brandInfo != null ? req.Scheme + Uri.SchemeDelimiter + req.Host.Value + brandInfo.Value : string.Empty;
                    imageField.IsDisplay = itemImage.IsDisplay;
                    imageField.X = itemImage.X;
                    imageField.Y = itemImage.Y;
                    imageField.Width = itemImage.Width;
                    imageField.Height = itemImage.Height;

                    lstImageField.Add(imageField);
                }

            }

            obj.ImageFields = lstImageField;

            var record = obj;

            var remainingFields = templateFileds.Select(x => x.Id)
                    .Except(record!.TextFields.Select(x => x.TemplateFieldId))
                    .Except(record!.ImageFields.Select(x => x.TemplateFieldId));

            var align = await _context.tblSCRMAlign.FirstOrDefaultAsync();
            var fontFamily = await _context.tblSCRMFontFamily.FirstOrDefaultAsync();

            foreach (var item in remainingFields)
            {
                var field = templateFileds.FirstOrDefault(x => x.Id == item);
                if (field != null)
                {
                    if (field.TemplateFieldTypeId == 1)
                    {
                        var objText = new SCRMTemplateFieldTextModel();

                        objText.Id = 0;
                        objText.TemplateId = templateId;
                        objText.TemplateName = null;
                        objText.TemplateFieldId = field.Id;
                        objText.TemplateFieldName = field.Name;
                        objText.Value = field.Value;
                        objText.FontFamilyId = fontFamily != null ? fontFamily.Id : 1;
                        objText.FontFamilyName = fontFamily != null ? fontFamily.Name : "Poppins";
                        objText.IsDisplay = false;
                        objText.X = 0;
                        objText.Y = 0;
                        objText.AlignId = align != null ? align.Id : 1;
                        objText.Align = align != null ? align.Name : "Left";
                        objText.Size = 10;
                        objText.Color = "#000000";

                        record!.TextFields.Add(objText);
                    }

                    if (field.TemplateFieldTypeId == 2)
                    {
                        var objImage = new SCRMTemplateFieldImageModel();
                        objImage.Id = 0;
                        objImage.TemplateId = templateId;
                        objImage.TemplateName = null;
                        objImage.TemplateFieldId = field.Id;
                        objImage.TemplateFieldName = field.Name;
                        objImage.Value = req.Scheme + Uri.SchemeDelimiter + req.Host.Value + field.Value;
                        objImage.IsDisplay = false;
                        objImage.X = 0;
                        objImage.Y = 0;
                        objImage.Width = 0;
                        objImage.Height = 0;
                        record!.ImageFields.Add(objImage);
                    }
                }
            }

            return record!;
        }

        public async Task<SCRMTemplateLayoutModel> UpdateTemplateLayoutAsync(int templateId, SCRMTemplateLayoutModel model)
        {
            var textFieldTBL = _context.tblSCRMTemplateFieldText;
            var imageFieldTBL = _context.tblSCRMTemplateFieldImage;
            var data = await _context.tblSCRMTemplate.FindAsync(templateId);
            if (data != null)
            {
                var existTextFields = textFieldTBL.Where(x => x.TemplateId == data.Id);
                if (existTextFields != null)
                    foreach (var field in existTextFields)
                    {
                        textFieldTBL.Remove(field);
                    }

                var existImageFields = imageFieldTBL.Where(x => x.TemplateId == data.Id);
                if (existImageFields != null)
                    foreach (var field in existImageFields)
                    {
                        imageFieldTBL.Remove(field);
                    }

                foreach (var field in model.TextFields)
                {
                    var textField = new SCRMTemplateFieldText()
                    {
                        TemplateId = templateId,
                        TemplateFieldId = field.TemplateFieldId,
                        FontFamilyId = field.FontFamilyId,
                        IsDisplay = field.IsDisplay,
                        X = field.X,
                        Y = field.Y,
                        AlignId = field.AlignId,
                        Size = field.Size,
                        Color = field.Color
                    };
                    textFieldTBL.Add(textField);
                }

                foreach (var field in model.ImageFields)
                {
                    var imageField = new SCRMTemplateFieldImage()
                    {
                        TemplateId = templateId,
                        TemplateFieldId = field.TemplateFieldId,
                        IsDisplay = field.IsDisplay,
                        X = field.X,
                        Y = field.Y,
                        Width = field.Width,
                        Height = field.Height
                    };
                    imageFieldTBL.Add(imageField);
                }
                await _context.SaveChangesAsync();
            }

            var record = await GetTemplateLayoutByIdAsync(data!.Id);
            return record;
        }

        public static async Task<IPagedList<SCRMTemplateLayoutModel>> SortResult(List<SCRMTemplateLayoutModel> source, SCRMRequestParams requestParams)
        {
            IOrderedEnumerable<SCRMTemplateLayoutModel> data = source.OrderBy(s => s.Id);
            if (requestParams.sortBy != null)
                if (requestParams.orderBy == "Desc" || requestParams.orderBy == "desc")
                    data = source.OrderByDescending(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));
                else
                    data = source.OrderBy(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));

            return await data.ToPagedListAsync(requestParams.pageNumber, requestParams.pageSize);
        }
    }
}
