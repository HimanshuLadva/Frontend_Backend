using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using X.PagedList;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class SCRMUserTemplateRepository : SCRMIUserTemplateRepository
    {
        private readonly WebsiteCMSDbContext _context;
        public readonly IWebHostEnvironment _webHostEnvironment;
        private IBaseRepository _baseRepository;

        public SCRMUserTemplateRepository(WebsiteCMSDbContext context, IWebHostEnvironment webHostEnvironment, IBaseRepository baseRepository)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _baseRepository = baseRepository;
        }

        public async Task<IPagedList<SCRMUserTemplateModel>> GetAllUserTemplateAsync(string userId, SCRMRequestParams requestParams)
        {
            var user = _baseRepository.GetUser();

            var templateField = _context.tblSCRMTemplateField.Where(x => x.IsActive == true).ToList();
            var textFieldTBL = _context.tblSCRMTemplateFieldText;
            var imageFieldTBL = _context.tblSCRMTemplateFieldImage;
            var userTBL = _context.Users;
            var templateTagTBL = _context.tblSCRMTemplateTag;
            var userMetadataTBL = _context.tblSCRMUserMetaData;
            var records = new List<SCRMUserTemplateModel>();

            var existUser = await _context.Users.Where(x => x.Id == _baseRepository.GetUserId()).FirstOrDefaultAsync();
            if (existUser != null)
            {
                var existTemp = await _context.tblSCRMTemplate.Where(x => x.IsActive == true).ToListAsync();
                var existTempTag = await _context.tblSCRMTemplateTag.ToListAsync();
                var existTemplates = new List<SCRMTemplate>();

                if (requestParams.Tags == null)
                {
                    existTemplates = await _context.tblSCRMTemplate.Where(x => x.IsActive == true).ToListAsync();
                }
                else
                {
                    string[] tags = requestParams.Tags.Split(',').Select(x => x.Trim()).ToArray();
                    foreach (string tag in tags)
                    {
                        var templateTag = existTempTag.Where(x => x.TagId == Convert.ToInt32(tag)).ToList();
                        foreach (var template in templateTag)
                        {
                            var temp = existTemp.Where(x => x.Id == template.TemplateId).FirstOrDefault();
                            if (temp != null)
                            {
                                existTemplates.Add(temp);
                            }
                        }
                    }
                }

                foreach (var template in existTemplates.Distinct())
                {
                    var templateRecord = await userTBL.Where(x => x.Id == _baseRepository.GetUserId())
                        .Select(x => new SCRMUserTemplateModel()
                        {
                            ApplicationUserId = _baseRepository.GetUserId(),
                            TemplateId = template.Id,
                            TemplateName = template.Name,
                            TemplateImageURL = template.TemplateImageURL,
                            CreatedDate = template.CreatedDate,
                            IsFree = template.IsFree,

                            Tags = templateTagTBL.Where(t => t.TemplateId == template.Id)
                            .Select(x => new SCRMTemplateTagModel()
                            {
                                Id = x.Id,
                                TemplateId = template.Id,
                                Template = x.Template.Name,
                                TagId = x.TagId,
                                Tag = x.Tag.Name
                            }).ToList(),

                            TextFields = textFieldTBL.Where(x => x.TemplateId == template.Id)
                            .Select(x => new SCRMTemplateFieldTextModel()
                            {
                                Id = x.Id,
                                TemplateId = x.TemplateId,
                                TemplateName = x.Template.Name,
                                TemplateFieldId = x.TemplateFieldId,
                                TemplateFieldName = x.TemplateField.Name,
                                FontFamilyId = x.FontFamilyId,
                                FontFamilyName = x.FontFamily.Name,
                                Value = userMetadataTBL.Where(t => t.ApplicationUserId == _baseRepository.GetUserId() && t.TemplateFieldId == x.TemplateFieldId).FirstOrDefault()!.Value,
                                IsDisplay = x.IsDisplay,
                                X = x.X,
                                Y = x.Y,
                                AlignId = x.AlignId,
                                Align = x.Align.Name,
                                Size = x.Size,
                                Color = x.Color
                            }).ToList(),

                            ImageFields = imageFieldTBL.Where(x => x.TemplateId == template.Id)
                            .Select(x => new SCRMTemplateFieldImageModel()
                            {
                                Id = x.Id,
                                TemplateId = x.TemplateId,
                                TemplateName = x.Template.Name,
                                TemplateFieldId = x.TemplateFieldId,
                                TemplateFieldName = x.TemplateField.Name,
                                Value = userMetadataTBL.Where(t => t.ApplicationUserId == _baseRepository.GetUserId() && t.TemplateFieldId == x.TemplateFieldId).FirstOrDefault()!.Value,
                                IsDisplay = x.IsDisplay,
                                X = x.X,
                                Y = x.Y,
                                Width = x.Width,
                                Height = x.Height
                            }).ToList(),
                        }).FirstOrDefaultAsync();
                    records.Add(templateRecord!);
                }
            }

            requestParams.recordCount = records.Count;

            var data = await SortResult(records, requestParams);
            return data;
        }

        public async Task<SCRMUserTemplateModel> GetUserTemplateByIdAsync(string userId, int templateId)
        {
            var record = await _context.Users.Where(x => x.Id == _baseRepository.GetUserId()).FirstOrDefaultAsync();
            var textFieldTBL = _context.tblSCRMTemplateFieldText;
            var imageFieldTBL = _context.tblSCRMTemplateFieldImage;
            var userMetadataTBL = _context.tblSCRMUserMetaData;
            var templateTBL = _context.tblSCRMTemplate;
            var templateTagTBL = _context.tblSCRMTemplateTag;

            if (record != null)
            {
                var templateData = templateTBL.Where(x => x.Id == templateId).FirstOrDefault();
                var data = new SCRMUserTemplateModel()
                {
                    ApplicationUserId = _baseRepository.GetUserId(),
                    TemplateId = templateId,
                    TemplateName = templateData!.Name,
                    TemplateImageURL = templateData!.TemplateImageURL,
                    CreatedDate = templateData!.CreatedDate,
                    IsFree = templateData!.IsFree,

                    Tags = templateTagTBL.Where(t => t.TemplateId == templateId)
                            .Select(x => new SCRMTemplateTagModel()
                            {
                                Id = x.Id,
                                TemplateId = templateId,
                                Template = x.Template.Name,
                                TagId = x.TagId,
                                Tag = x.Tag.Name
                            }).ToList(),

                    TextFields = textFieldTBL.Where(a => a.TemplateId == templateId)
                    .Select(x => new SCRMTemplateFieldTextModel()
                    {
                        Id = x.Id,
                        TemplateId = x.TemplateId,
                        TemplateName = x.Template.Name,
                        TemplateFieldId = x.TemplateFieldId,
                        TemplateFieldName = x.TemplateField.Name,
                        FontFamilyId = x.FontFamilyId,
                        FontFamilyName = x.FontFamily.Name,
                        Value = userMetadataTBL.Where(t => t.ApplicationUserId == _baseRepository.GetUserId() && t.TemplateFieldId == x.TemplateFieldId).FirstOrDefault()!.Value,
                        IsDisplay = x.IsDisplay,
                        X = x.X,
                        Y = x.Y,
                        AlignId = x.AlignId,
                        Align = x.Align.Name,
                        Size = x.Size,
                        Color = x.Color
                    }).ToList(),

                    ImageFields = imageFieldTBL.Where(a => a.TemplateId == templateId)
                    .Select(x => new SCRMTemplateFieldImageModel()
                    {
                        Id = x.Id,
                        TemplateId = x.TemplateId,
                        TemplateName = x.Template.Name,
                        TemplateFieldId = x.TemplateFieldId,
                        TemplateFieldName = x.TemplateField.Name,
                        Value = userMetadataTBL.Where(t => t.ApplicationUserId == _baseRepository.GetUserId() && t.TemplateFieldId == x.TemplateFieldId).FirstOrDefault()!.Value,
                        IsDisplay = x.IsDisplay,
                        X = x.X,
                        Y = x.Y,
                        Width = x.Width,
                        Height = x.Height
                    }).ToList(),
                };
                return data;
            }
            return null!;
        }

        public static async Task<IPagedList<SCRMUserTemplateModel>> SortResult(List<SCRMUserTemplateModel> source, SCRMRequestParams requestParams)
        {
            IOrderedEnumerable<SCRMUserTemplateModel> data = source.OrderByDescending(s => s.CreatedDate);
            if (requestParams.sortBy != null)
                if (requestParams.orderBy == "Desc" || requestParams.orderBy == "desc")
                    data = source.OrderByDescending(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));
                else
                    data = source.OrderBy(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));

            return await data.ToPagedListAsync(requestParams.pageNumber, requestParams.pageSize);
        }
    }
}
