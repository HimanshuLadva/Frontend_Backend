using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NPOI.POIFS.Crypt.Dsig;
using Org.BouncyCastle.Asn1.Ocsp;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using X.PagedList;
using Microsoft.AspNetCore.Http.Extensions;
using System.Drawing;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class SCRMTemplateFieldRepository : SCRMITemplateFieldRepository
    {
        private readonly WebsiteCMSDbContext _context;
        public readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAWSImageService _imageRepository;
        private static string FolderName = "SCRMTemplateField";

        public SCRMTemplateFieldRepository(WebsiteCMSDbContext context, IWebHostEnvironment webHostEnvironment, IAWSImageService imageRepository)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imageRepository = imageRepository;
        }

        public async Task<IPagedList<SCRMTemplateFieldModel>> GetAllTemplateFieldAsync(SCRMRequestParams requestParams, string baseURL)
        {
            var records = new List<SCRMTemplateFieldModel>();
            records = await _context.tblSCRMTemplateField
                .Select(x => new SCRMTemplateFieldModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    TemplateFieldTypeId = x.TemplateFieldTypeId,
                    FieldType = x.TemplateFieldType.Name,
                    Value = x.Value,
                    IsActive = x.IsActive
                }).ToListAsync();

            if (requestParams.search != null)
                records = await records.Where(x => x.Name.ToLower().Contains(requestParams.search.ToLower())).ToListAsync();

            if (requestParams.isActive == "Active" || requestParams.isActive == "active")
                records = await records.Where(x => x.IsActive == true).ToListAsync();

            requestParams.recordCount = records.Count;

            var data = await SortResult(records, requestParams);
            return data;
        }

        public async Task<SCRMTemplateFieldModel> GetTemplateFieldByIdAsync(int id)
        {
            var record = await _context.tblSCRMTemplateField.Where(x => x.Id == id)
               .Select(x => new SCRMTemplateFieldModel()
               {
                   Id = id,
                   Name = x.Name,
                   TemplateFieldTypeId = x.TemplateFieldTypeId,
                   FieldType = x.TemplateFieldType.Name,
                   Value = x.Value,
                   IsActive = x.IsActive
               }).FirstOrDefaultAsync();
            return record!;
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

        public async Task<SCRMTemplateFieldModel> AddTemplateFieldAsync(SCRMTemplateFieldModel model)
        {
            var userTBL = _context.Users;
            string imageUrl = string.Empty;
            var userMetadataTBL = _context.tblSCRMUserMetaData;

            if (model.TemplateFieldTypeId == 2 && model.FieldImage != null)
            {
                bool isAspectRatioAllow = CheckAspectRatio(model.FieldImage!);
                if (isAspectRatioAllow == true)
                {
                    await using var memoryStream = new MemoryStream();
                    await model.FieldImage!.CopyToAsync(memoryStream);
                    string docName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(model.FieldImage.FileName));
                    imageUrl = await _imageRepository.UploadFileAsync(memoryStream, docName, AWSDirectory.SCRMTemplateField);
                    model.Value = docName;
                }
                else
                    throw new Exception("Please Enter Valid Image For Template Field...!");
            }

            var data = new SCRMTemplateField()
            {
                Name = model.Name,
                TemplateFieldTypeId = model.TemplateFieldTypeId,
                Value = model.Value,
                UserMetaData = new List<SCRMUserMetaData>()
            };

            var userList = userTBL.Select(x => x.Id);
            if (userList != null)
                foreach (string user in userList)
                {
                    data.UserMetaData!.Add(new SCRMUserMetaData()
                    {
                        ApplicationUserId = user.ToString()
                    });
                }

            _context.tblSCRMTemplateField.Add(data);
            await _context.SaveChangesAsync();

            var record = await GetTemplateFieldByIdAsync(data.Id);
            return record;
        }

        public async Task<SCRMTemplateFieldModel> UpdateTemplateFieldAsync(int id, SCRMTemplateFieldModel model)
        {
            string imageUrl = string.Empty;
            var data = await _context.tblSCRMTemplateField.FindAsync(id);
            if (model.TemplateFieldTypeId == 2 && model.FieldImage != null)
            {
                bool isAspectRatioAllow = CheckAspectRatio(model.FieldImage!);
                if (isAspectRatioAllow == true)
                {
                    await using var memoryStream = new MemoryStream();

                    if (model.FieldImage != null)
                    {
                        await model.FieldImage!.CopyToAsync(memoryStream);
                        bool isDeletedOrNot = await _imageRepository.DeleteImageAsync(data.Value!);
                        string docName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(model.FieldImage.FileName));
                        imageUrl = await _imageRepository.UploadFileAsync(memoryStream, docName, AWSDirectory.SCRMTemplateField);
                    }
                    model.Value = imageUrl;
                }
                else
                    throw new Exception("Please Enter Valid Image For Template Field...!");
            }

            if (data != null)
            {
                data.Id = id;
                data.Name = model.Name;
                data.IsActive = model.IsActive;
                data.TemplateFieldTypeId = model.TemplateFieldTypeId;
                if (model.Value != null)
                    data.Value = model.Value;
                data.CreatedDate = data.CreatedDate;
                data.UpdatedDate = DateTime.Now;
                data.IsDeleted = data.IsDeleted;

                _context.tblSCRMTemplateField.Update(data);
                await _context.SaveChangesAsync();

                var record = await GetTemplateFieldByIdAsync(data.Id);
                return record;
            }
            return null!;
        }

        public async Task<bool> UpdateTemplateFieldStatusAsync(int id, SCRMUpdateStatusModel model)
        {
            var data = await _context.tblSCRMTemplateField.FindAsync(id);
            if (data != null)
            {
                data.IsActive = model.IsActive;
                data.UpdatedDate = DateTime.Now;

                _context.tblSCRMTemplateField.Update(data);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task DeleteTemplateFieldAsync(int id)
        {
            var textFieldTBL = _context.tblSCRMTemplateFieldText;
            var imageFieldTBL = _context.tblSCRMTemplateFieldImage;

            var record = _context.tblSCRMTemplateField.Where(x => x.Id == id).FirstOrDefault();
            bool isDeleted = await _imageRepository.DeleteImageAsync(record!.Value!);
            _context.tblSCRMTemplateField.Remove(record);

            var existTextField = _context.tblSCRMTemplateFieldText.Where(x => x.TemplateFieldId == id);
            var existImageField = _context.tblSCRMTemplateFieldImage.Where(x => x.TemplateFieldId == id);

            if (existTextField != null)
                foreach (var field in existTextField)
                {
                    textFieldTBL.Remove(field);
                }

            if (existImageField != null)
                foreach (var field in existImageField)
                {
                    imageFieldTBL.Remove(field);
                }

            await _context.SaveChangesAsync();
        }

        public static async Task<IPagedList<SCRMTemplateFieldModel>> SortResult(List<SCRMTemplateFieldModel> source, SCRMRequestParams requestParams)
        {
            IOrderedEnumerable<SCRMTemplateFieldModel> data = source.OrderBy(s => s.Name);
            if (requestParams.sortBy != null)
                if (requestParams.orderBy == "Desc" || requestParams.orderBy == "desc")
                    data = source.OrderByDescending(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));
                else
                    data = source.OrderBy(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));

            return await data.ToPagedListAsync(requestParams.pageNumber, requestParams.pageSize);
        }
    }
}
