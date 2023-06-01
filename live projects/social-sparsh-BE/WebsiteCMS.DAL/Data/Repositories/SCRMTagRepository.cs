using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System.Drawing;
using System.IO;
using System.Linq;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using WebsiteCMS.Global.Configurations;
using X.PagedList;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class SCRMTagRepository : SCRMITagRepository
    {
        private readonly WebsiteCMSDbContext _context;
        private SCRMITemplateLayoutRepository _templateLayoutRepository;
        private readonly IAWSImageService _imageRepository;

        public SCRMTagRepository(WebsiteCMSDbContext context, SCRMITemplateLayoutRepository templateLayoutRepository, IAWSImageService imageRepository)
        {
            _context = context;
            _templateLayoutRepository = templateLayoutRepository;
            _imageRepository = imageRepository;
        }

        public async Task<IPagedList<SCRMTagModel>> GetAllTagAsync(SCRMRequestParams requestParams)
        {
            var records = new List<SCRMTagModel>();
            records = await _context.tblSCRMTag
                   .Select(x => new SCRMTagModel()
                   {
                       Id = x.Id,
                       Name = x.Name,
                       IsActive = x.IsActive,
                       TagImageUrl = x.TagImage
                   }).ToListAsync();

            if (requestParams.search != null)
                records = await records.Where(x => x.Name.ToLower().Contains(requestParams.search.ToLower())).ToListAsync();

            if (requestParams.isActive == "Active" || requestParams.isActive == "active")
                records = await records.Where(x => x.IsActive == true).ToListAsync();

            requestParams.recordCount = records.Count;

            var data = await SortResult(records, requestParams);
            return data;
        }

        public async Task<List<SCRMTagModel>> GetAllActiveTagAsync()
        {
            var records = new List<SCRMTagModel>();
            records = await _context.tblSCRMTag
                .Where(x => x.IsActive == true)
                   .Select(x => new SCRMTagModel()
                   {
                       Id = x.Id,
                       Name = x.Name,
                       IsActive = x.IsActive,
                       TagImageUrl = x.TagImage
                   }).ToListAsync();
            return records;
        }

        public async Task<SCRMTagModel> GetTagByIdAsync(int id)
        {
            var record = await _context.tblSCRMTag.Where(x => x.Id == id)
                .Select(x => new SCRMTagModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsActive = x.IsActive,
                    TagImageUrl = x.TagImage
                }).FirstOrDefaultAsync();
            return record!;
        }

        public async Task<SCRMTagModel> AddTagAsync(SCRMTagModel model)
        {
            //string imagePath = await UploadImage(model.TagImage);
            await using var memoryStream = new MemoryStream();
            await model.TagImage!.CopyToAsync(memoryStream);
            string docName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(model.TagImage.FileName));
            string imageUrl = await _imageRepository.UploadFileAsync(memoryStream, docName, AWSDirectory.SCRMTag);

            if (imageUrl != null)
            {
                var data = new SCRMTag()
                {
                    Name = model.Name,
                    TagImage = imageUrl
                };

                _context.tblSCRMTag.Add(data);
                await _context.SaveChangesAsync();

                var record = await GetTagByIdAsync(data.Id);
                return record;
            }
            return null!;
        }

        public async Task<SCRMTagModel> UpdateTagAsync(int id, SCRMTagModel model)
        {
            var data = await _context.tblSCRMTag.FindAsync(id);
            string imageUrl = "";
            await using var memoryStream = new MemoryStream();

            if (model.TagImage != null)
            {
                await model.TagImage!.CopyToAsync(memoryStream);
                if (string.IsNullOrEmpty(data!.TagImage))
                {
                    bool isDeletedOrNot = await _imageRepository.DeleteImageAsync(data.TagImage!);
                }
                string docName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(model.TagImage.FileName));
                imageUrl = await _imageRepository.UploadFileAsync(memoryStream, docName, AWSDirectory.SCRMTag);
            }
            if (data != null)
            {
                data.Id = id;
                data.Name = model.Name;
                data.IsActive = model.IsActive;
                data.CreatedDate = data.CreatedDate;
                data.UpdatedDate = DateTime.Now;
                data.IsDeleted = data.IsDeleted;

                if (imageUrl != null)
                {
                    data.TagImage = imageUrl;
                }

                _context.tblSCRMTag.Update(data);
                await _context.SaveChangesAsync();

                var record = await GetTagByIdAsync(data.Id);
                return record;
            }

            return null!;
        }

        public async Task<bool> UpdateTagStatusAsync(int id, SCRMUpdateStatusModel model)
        {
            var data = await _context.tblSCRMTag.FindAsync(id);
            if (data != null)
            {
                data.IsActive = model.IsActive;
                data.UpdatedDate = DateTime.Now;

                _context.tblSCRMTag.Update(data);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task DeleteTagAsync(int id)
        {
            var record = _context.tblSCRMTag.Where(x => x.Id == id).FirstOrDefault();
            bool isDeleted = await _imageRepository.DeleteImageAsync(record!.TagImage!);
            _context.tblSCRMTag.Remove(record!);

            var existTag = _context.tblSCRMTemplateTag.Where(x => x.TagId == id);
            if (existTag != null)
                foreach (var field in existTag)
                {
                    _context.tblSCRMTemplateTag.Remove(field);
                }

            await _context.SaveChangesAsync();
        }

        public async Task<List<SCRMTagWiseTemplateModel>> GetAllTagWiseTemplateListAsync()
        {
            var lstTagModel = new List<SCRMTagWiseTemplateModel>();
            var tags = await _context.tblSCRMTag.Where(x => x.IsActive == true).ToListAsync();

            foreach (var item in tags)
            {
                var objTag = new SCRMTagWiseTemplateModel();
                var templateModel = new List<SCRMTemplateWithLayoutModel>();

                objTag.Id = item.Id;
                objTag.Name = item.Name;

                var tagWiseTemplate = await _context.tblSCRMTemplateTag.Include(x => x.Template).ThenInclude(x => x.Category).Where(x => x.TagId == item.Id).ToListAsync();
                foreach (var tagTemplate in tagWiseTemplate)
                {
                    if (tagTemplate.Template != null)
                    {
                        var obj = new SCRMTemplateWithLayoutModel();

                        obj.Id = tagTemplate.Template.Id;
                        obj.Name = tagTemplate.Template.Name;
                        //obj.CategoryId = tagTemplate.Template.CategoryId;
                        //obj.Category = tagTemplate.Template.Category.Name;
                        obj.TemplateImageURL = tagTemplate.Template.TemplateImageURL;
                        obj.IsActive = tagTemplate.Template.IsActive;
                        obj.IsFree = tagTemplate.Template.IsFree;

                        var templateLayout = await _templateLayoutRepository.GetTemplateLayoutByIdAsync(tagTemplate.Template.Id);

                        if (templateLayout != null)
                        {
                            obj.TextFields = templateLayout.TextFields;
                            obj.ImageFields = templateLayout.ImageFields;
                        }
                        templateModel.Add(obj);
                    }
                }
                objTag.TemplateAndLayout = templateModel;

                lstTagModel.Add(objTag);
            }

            return lstTagModel;
        }

        public static async Task<IPagedList<SCRMTagModel>> SortResult(List<SCRMTagModel> source, SCRMRequestParams requestParams)
        {
            IOrderedEnumerable<SCRMTagModel> data = source.OrderBy(s => s.Name);
            if (requestParams.sortBy != null)
                if (requestParams.orderBy == "Desc" || requestParams.orderBy == "desc")
                    data = source.OrderByDescending(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));
                else
                    data = source.OrderBy(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));

            return await data.ToPagedListAsync(requestParams.pageNumber, requestParams.pageSize);
        }
    }
}
