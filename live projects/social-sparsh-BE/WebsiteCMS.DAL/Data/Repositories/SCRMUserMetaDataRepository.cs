using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using NPOI.POIFS.Crypt.Dsig;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using X.PagedList;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class SCRMUserMetaDataRepository : SCRMIUserMetaDataRepository
    {
        private readonly WebsiteCMSDbContext _context;
        public readonly IWebHostEnvironment _webHostEnvironment;
        protected IBaseRepository _baseRepository;
        private readonly IAWSImageService _imageRepository;

        public SCRMUserMetaDataRepository(WebsiteCMSDbContext context, IWebHostEnvironment webHostEnvironment, IBaseRepository baseRepository, IAWSImageService imageRepository)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _baseRepository = baseRepository;
            _imageRepository = imageRepository;
        }

        public async Task<IPagedList<SCRMUserMetaDataModel>> GetAllUserMetaDataAsync(SCRMRequestParams requestParams)
        {
            var records = new List<SCRMUserMetaDataModel>();
            records = await _context.tblSCRMUserMetaData
                   .Select(x => new SCRMUserMetaDataModel()
                   {
                       Id = x.Id,
                       ApplicationUserId = x.ApplicationUserId,
                       TemplateFieldId = x.TemplateFieldId,
                       FieldType = x.TemplateField.TemplateFieldType.Name,
                       Name = x.TemplateField.Name,
                       Value = x.Value
                   }).ToListAsync();

            requestParams.recordCount = records.Count;

            var data = await SortResult(records, requestParams);
            return data;
        }

        public async Task<List<SCRMUserMetaDataModel>> GetUserMetaDataByIdAsync(string userId)
        {
            //var record = new List<SCRMUserMetaDataModel>();
            //record = await _context.tblSCRMUserMetaData.Where(x => x.ApplicationUserId == _baseRepository.GetUserId())
            //    .Select(x => new SCRMUserMetaDataModel()
            //    {
            //        Id = x.Id,
            //        ApplicationUserId = _baseRepository.GetUserId(),
            //        TemplateFieldId = x.TemplateFieldId,
            //        FieldType = x.TemplateField.TemplateFieldType.Name,
            //        Name = x.TemplateField.Name,
            //        Value = x.Value!
            //    }).ToListAsync();
            //return record;

            var record = new List<SCRMUserMetaDataModel>();
            var templateFileds = await _context.tblSCRMTemplateField.Include(x => x.TemplateFieldType).Where(x => x.IsActive == true).ToListAsync();
            var userBrandInfo = await _context.tblSCRMUserMetaData.Include(x => x.TemplateField).ThenInclude(x => x.TemplateFieldType)
                .Where(x => x.ApplicationUserId == _baseRepository.GetUserId()).ToListAsync();

            foreach (var item in templateFileds)
            {
                var brandInfo = userBrandInfo.FirstOrDefault(x => x.TemplateFieldId == item.Id);
                SCRMUserMetaDataModel obj = new();

                obj.Id = brandInfo != null ? brandInfo.Id : 0;
                obj.ApplicationUserId = _baseRepository.GetUserId();
                obj.TemplateFieldId = brandInfo != null ? brandInfo.TemplateFieldId : item.Id;
                obj.FieldType = brandInfo != null ? brandInfo.TemplateField.TemplateFieldType.Name : item.TemplateFieldType.Name;
                obj.Name = brandInfo != null ? brandInfo.TemplateField.Name : item.Name;
                obj.Value = brandInfo != null ? _baseRepository.GetImageBaseUrl() + brandInfo.Value! : string.Empty;

                record.Add(obj);

            }

            return record;
        }

        public static bool CheckAspectRatio(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                using (var img = Image.FromStream(memoryStream))
                {
                    if (img.Width == img.Height)
                        return true;
                    else
                        return false;
                };
            };
        }
        public async Task<SCRMUserMetaData> AddUserMetaDataAsync(IFormCollection model, string userId)
        {
            var userMetadataTBL = _context.tblSCRMUserMetaData;
            string imageUrl = string.Empty;

            foreach (var key in model.Keys)
            {
                var data = new SCRMUserMetaData()
                {
                    ApplicationUserId = _baseRepository.GetUserId(),
                    TemplateFieldId = Convert.ToInt32(key),
                    Value = model[key]
                };
                userMetadataTBL.Add(data);
            }

            foreach (var file in model.Files)
            {
                var path = "";
                if (file != null)
                {
                    await using var memoryStream = new MemoryStream();
                    await file!.CopyToAsync(memoryStream);
                    string docName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(file.FileName));
                    imageUrl = await _imageRepository.UploadFileAsync(memoryStream, docName, AWSDirectory.SCRMUserMetaData);
                    path = docName;
                }

                var data = new SCRMUserMetaData()
                {
                    ApplicationUserId = _baseRepository.GetUserId(),
                    TemplateFieldId = Convert.ToInt32(file!.Name),
                    Value = imageUrl
                };
                userMetadataTBL.Add(data);
            }

            await _context.SaveChangesAsync();
            return null!;
        }

        public async Task<List<SCRMUserMetaDataModel>> UpdateUserMetaDataAsync(string userId, IFormCollection model)
        {
            var existRecords = await _context.tblSCRMUserMetaData.Where(x => x.ApplicationUserId == _baseRepository.GetUserId()).ToListAsync();
            var userMetadata = await _context.tblSCRMUserMetaData.ToListAsync();
            var userMetadataTBL = _context.tblSCRMUserMetaData;
            var path = "";

            if (existRecords != null)
            {
                foreach (var key in model.Keys)
                {
                    if (Convert.ToInt32(key) != 0)
                    {
                        var data = userMetadata.Where(x => x.ApplicationUserId == _baseRepository.GetUserId() && x.TemplateFieldId == Convert.ToInt32(key)).FirstOrDefault();
                        if (data != null && model[key] != "")
                        {
                            data.Value = model[key];
                            userMetadataTBL.Update(data);
                        }
                    }
                }

                foreach (var file in model.Files)
                {
                    if (file != null)
                    {
                        var data = userMetadata.Where(x => x.ApplicationUserId == _baseRepository.GetUserId() && x.TemplateFieldId == Convert.ToInt32(file.Name)).FirstOrDefault();
                        if (data != null)
                        {
                            await using var memoryStream = new MemoryStream();
                            await file!.CopyToAsync(memoryStream);
                            bool isDeletedOrNot = await _imageRepository.DeleteImageAsync(data.Value!);
                            string docName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(file.FileName));
                            string imageUrl = await _imageRepository.UploadFileAsync(memoryStream, docName, AWSDirectory.SCRMUserMetaData);
                            data.Value = imageUrl;
                            userMetadataTBL.Update(data);
                        }
                    }
                }
                await _context.SaveChangesAsync();
                var record = await GetUserMetaDataByIdAsync(userId);
                return record;
            }
            return null!;
        }

        public async Task<List<SCRMUserMetaDataModel>> AddUpdateUserMetaDataAsync(IFormCollection model)
        {
            var userId = _baseRepository.GetUserId();

            var templateFields = await _context.tblSCRMTemplateField.ToListAsync();
            var templateType = _context.tblSCRMTemplateFieldType.FirstOrDefault(x => x.Name == "Image");

            var existRecords = await _context.tblSCRMUserMetaData.Where(x => x.ApplicationUserId == userId).ToListAsync();
            var path = "";

            foreach (var key in model.Keys)
            {
                if (Convert.ToInt32(key) != 0 && !IsImageField(templateFields, Convert.ToInt32(key), templateType!))
                {
                    var data = existRecords.Where(x => x.TemplateFieldId == Convert.ToInt32(key)).FirstOrDefault();
                    if (data != null && model[key] != "")
                    {
                        data.Value = model[key];
                        _context.tblSCRMUserMetaData.Update(data);
                    }
                    else
                    {
                        var dataKey = new SCRMUserMetaData()
                        {
                            ApplicationUserId = userId,
                            TemplateFieldId = Convert.ToInt32(key),
                            Value = model[key]
                        };
                        _context.tblSCRMUserMetaData.Add(dataKey);
                    }
                }
            }

            foreach (var file in model.Files)
            {
                if (file != null)
                {
                    var data = existRecords.Where(x => x.TemplateFieldId == Convert.ToInt32(file.Name)).FirstOrDefault();
                    if (data != null)
                    {
                        await using var memoryStream = new MemoryStream();
                        await file!.CopyToAsync(memoryStream);
                        string docName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(file.FileName));
                        string imageUrl = await _imageRepository.UploadFileAsync(memoryStream, docName, AWSDirectory.SCRMUserMetaData);
                        data.Value = imageUrl;
                        _context.tblSCRMUserMetaData.Update(data);
                    }
                    else
                    {
                        string imageUrl = string.Empty;
                        await using var memoryStream = new MemoryStream();

                        if (file != null)
                        {
                            await file!.CopyToAsync(memoryStream);
                            bool isDeletedOrNot = await _imageRepository.DeleteImageAsync(data!.Value!);
                            string docName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(file.FileName));
                            imageUrl = await _imageRepository.UploadFileAsync(memoryStream, docName, AWSDirectory.SCRMUserMetaData);
                        }
                        var dataKey = new SCRMUserMetaData()
                        {
                            ApplicationUserId = userId,
                            TemplateFieldId = Convert.ToInt32(file!.Name),
                            Value = imageUrl
                        };
                        _context.tblSCRMUserMetaData.Add(dataKey);
                    }
                }
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
            }
            var record = await GetUserMetaDataByIdAsync(string.Empty);
            return record;
        }

        private bool IsImageField(List<SCRMTemplateField> templateFields, int Key, SCRMTemplateFieldType templateFieldType)
        {
            bool isImageField = false;
            var objField = templateFields.FirstOrDefault(x => x.Id == Key);
            if (objField != null && templateFieldType != null)
            {
                isImageField = objField.TemplateFieldTypeId == templateFieldType.Id;
            }
            return isImageField;
        }

        public static async Task<IPagedList<SCRMUserMetaDataModel>> SortResult(List<SCRMUserMetaDataModel> source, SCRMRequestParams requestParams)
        {
            IOrderedEnumerable<SCRMUserMetaDataModel> data = source.OrderBy(s => s.Id);
            if (requestParams.sortBy != null)
                if (requestParams.orderBy == "Desc" || requestParams.orderBy == "desc")
                    data = source.OrderByDescending(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));
                else
                    data = source.OrderBy(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));

            return await data.ToPagedListAsync(requestParams.pageNumber, requestParams.pageSize);
        }
    }
}
