using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Database.DBRepository;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.SCRMRequestModel;
using WebsiteCMS.Global.Configurations;
using X.PagedList;

namespace WebsiteCMS.DAL.Data.Repositories
{
    public class SCRMSubCategoryRepository : Repository<SCRMSubCategory>, SCRMISubCategoryRepository
    {
        private readonly WebsiteCMSDbContext _context;
        private readonly IBaseRepository _baseRepository;
        private readonly SCRMITemplateLayoutRepository _templateLayoutRepository;
        private readonly IAWSImageService _imageRepository;

        public SCRMSubCategoryRepository(WebsiteCMSDbContext context, IBaseRepository baseRepository, SCRMITemplateLayoutRepository templateLayoutRepository, IAWSImageService imageRepository) : base(context)
        {
            _context = context;
            _baseRepository = baseRepository;
            _templateLayoutRepository = templateLayoutRepository;
            _imageRepository = imageRepository;
        }

        public async Task<IPagedList<SCRMSubCategoryModel>> GetAllSubCategoryAsync(SCRMRequestParams requestParams)
        {
            var records = new List<SCRMSubCategoryModel>();
            records = await _context.tblSCRMSubCategory
                    .Select(x => new SCRMSubCategoryModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        EventDate = x.EventDate,
                        IsActive = x.IsActive,
                        CategoryId = x.CategoryId,
                        CategoryName = x.Category.Name,
                        SubCategoryImageURL = !string.IsNullOrWhiteSpace(x.SubCategoryImage) ? _imageRepository.GetImageBaseUrl() + x.SubCategoryImage : string.Empty
                    }).ToListAsync();

            if (requestParams.search != null)
                records = await records.Where(x => x.Name.ToLower().Contains(requestParams.search.ToLower())).ToListAsync();

            if (requestParams.isActive == "Active" || requestParams.isActive == "active")
                records = await records.Where(x => x.IsActive == true).ToListAsync();

            requestParams.recordCount = records.Count;

            var data = await SortResult(records, requestParams);
            return data;
        }

        public async Task<List<SCRMSubCategoryModel>> GetAllActiveSubCategoryAsync()
        {
            var records = new List<SCRMSubCategoryModel>();
            records = await _context.tblSCRMSubCategory
                .Where(x => x.IsActive == true)
                    .Select(x => new SCRMSubCategoryModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        EventDate = x.EventDate,
                        IsActive = x.IsActive,
                        CategoryId = x.CategoryId,
                        CategoryName = x.Category.Name,
                        SubCategoryImageURL = !string.IsNullOrWhiteSpace(x.SubCategoryImage) ? _imageRepository.GetImageBaseUrl() + x.SubCategoryImage : string.Empty
                    }).ToListAsync();
            return records;
        }

        public async Task<SCRMSubCategoryModel> GetSubCategoryByIdAsync(int id)
        {
            var records = await _context.tblSCRMSubCategory
                .Where(x => x.Id == id)
                .Select(x => new SCRMSubCategoryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    EventDate = x.EventDate,
                    IsActive = x.IsActive,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                    SubCategoryImageURL = !string.IsNullOrWhiteSpace(x.SubCategoryImage) ? _imageRepository.GetImageBaseUrl() + x.SubCategoryImage : string.Empty
                }).FirstOrDefaultAsync();
            return records!;
        }

        public async Task<SCRMSubCategoryModel> AddSubCategoryAsync(SCRMSubCategoryModel model)
        {
            await using var memoryStream = new MemoryStream();
            await model.SubCategoryImage!.CopyToAsync(memoryStream);
            string docName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(model.SubCategoryImage!.FileName));
            string imageUrl = await _imageRepository.UploadFileAsync(memoryStream, docName, AWSDirectory.SCRMSubCategory);

            var data = new SCRMSubCategory()
            {
                Name = model.Name,
                EventDate = model.EventDate,
                CategoryId = model.CategoryId,
                SubCategoryImage = imageUrl,
            };

            _context.tblSCRMSubCategory.Add(data);
            await _context.SaveChangesAsync();

            var record = await GetSubCategoryByIdAsync(data.Id);
            return record;
        }

        public async Task<SCRMSubCategoryModel> UpdateSubCategoryAsync(int id, SCRMSubCategoryModel model)
        {
            var data = await _context.tblSCRMSubCategory.FindAsync(id);
            string imageUrl = "";
            await using var memoryStream = new MemoryStream();

            if (model.SubCategoryImage != null)
            {
                await model.SubCategoryImage!.CopyToAsync(memoryStream);
                bool isDeletedOrNot = await _imageRepository.DeleteImageAsync(data.SubCategoryImage!);
                string docName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(model.SubCategoryImage!.FileName));
                imageUrl = await _imageRepository.UploadFileAsync(memoryStream, docName, AWSDirectory.SCRMSubCategory);
            }
            if (data != null)
            {
                if (!await _imageRepository.IsS3FileExists(data!.SubCategoryImage!))
                {
                    _ = await _imageRepository.DeleteImageAsync(data!.SubCategoryImage!);
                }
                data.Id = id;
                data.Name = model.Name;
                data.EventDate = model.EventDate;
                data.IsActive = model.IsActive;
                data.CreatedDate = data.CreatedDate;
                data.UpdatedDate = DateTime.Now;
                data.IsDeleted = data.IsDeleted;
                data.CategoryId = model.CategoryId;

                if (model.SubCategoryImage != null)
                {
                    data.SubCategoryImage = imageUrl;
                }

                _context.tblSCRMSubCategory.Update(data);
                await _context.SaveChangesAsync();

                var record = await GetSubCategoryByIdAsync(data.Id);
                return record;
            }
            return null!;
        }

        public async Task<bool> UpdateSubCategoryStatusAsync(int id, SCRMUpdateStatusModel model)
        {
            var data = await _context.tblSCRMSubCategory.FindAsync(id);
            if (data != null)
            {
                data.IsActive = model.IsActive;
                data.UpdatedDate = DateTime.Now;

                _context.tblSCRMSubCategory.Update(data);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task DeleteSubCategoryAsync(int id)
        {
            var record = _context.tblSCRMSubCategory.Where(x => x.Id == id).FirstOrDefault();
            if (record != null)
            {
                bool isDeleted = await _imageRepository.DeleteImageAsync(record!.SubCategoryImage!);
                _context.tblSCRMSubCategory.Remove(record!);
            }
            await _context.SaveChangesAsync();
        }

        public static async Task<IPagedList<SCRMSubCategoryModel>> SortResult(List<SCRMSubCategoryModel> source, SCRMRequestParams requestParams)
        {
            IOrderedEnumerable<SCRMSubCategoryModel> data = source.OrderBy(s => s.Name);
            if (requestParams.sortBy != null)
                if (requestParams.orderBy == "Desc" || requestParams.orderBy == "desc")
                    data = source.OrderByDescending(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));
                else
                    data = source.OrderBy(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));

            return await data.ToPagedListAsync(requestParams.pageNumber, requestParams.pageSize);
        }

        public async Task<List<SCRMSubCategroyWiseTemplateModel>> GetSubCategoryWiseTemplateListAsync(int id)
        {
            var lstSubCategoryModel = new List<SCRMSubCategroyWiseTemplateModel>();
            //var category = await _context.tblSCRMCategory.ToListAsync().Result.Select(x => x.IsActive == true && x.Id == id);
            var subcategory = await GetByIDAsync(id);


            var objCat = new SCRMSubCategroyWiseTemplateModel();
            var templateModel = new List<SCRMTemplateWithLayoutModel>();

            objCat.Id = subcategory!.Id;
            objCat.Name = subcategory.Name;

            var SubCatWiseTemplate = await _context.tblSCRMTemplateSubCategory.Include(x => x.Template).ThenInclude(x => x.SubCategory).Where(x => x.SubCategoryId == subcategory.Id).ToListAsync();
            foreach (var SubCatTemplate in SubCatWiseTemplate)
            {
                if (SubCatTemplate.Template != null)
                {
                    var obj = new SCRMTemplateWithLayoutModel();

                    obj.Id = SubCatTemplate.Template.Id;
                    obj.Name = SubCatTemplate.Template.Name;
                    obj.TemplateImageURL = SubCatTemplate.Template.TemplateImageURL;
                    obj.IsActive = SubCatTemplate.Template.IsActive;
                    obj.IsFree = SubCatTemplate.Template.IsFree;

                    var templateLayout = await _templateLayoutRepository.GetTemplateLayoutByIdAsync(SubCatTemplate.Template.Id);

                    if (templateLayout != null)
                    {
                        obj.TextFields = templateLayout.TextFields;
                        obj.ImageFields = templateLayout.ImageFields;
                    }

                    templateModel.Add(obj);
                }
            }
            objCat.TemplateAndLayout = templateModel;

            lstSubCategoryModel.Add(objCat);

            return lstSubCategoryModel;
        }

        public async Task<List<SCRMSubCategroyWiseTemplateModel>> GetAllSubCategoryWiseTemplateListAsync()
        {
            var lstCategoryModel = new List<SCRMSubCategroyWiseTemplateModel>();
            var categories = await _context.tblSCRMSubCategory.Where(x => x.IsActive == true).ToListAsync();

            foreach (var item in categories)
            {
                var objCat = new SCRMSubCategroyWiseTemplateModel();
                var templateModel = new List<SCRMTemplateWithLayoutModel>();

                objCat.Id = item.Id;
                objCat.Name = item.Name;

                var SubCatWiseTemplate = await _context.tblSCRMTemplateSubCategory.Include(x => x.Template).ThenInclude(x => x.SubCategory).Where(x => x.SubCategoryId == item.Id).ToListAsync();
                foreach (var SubCatTemplate in SubCatWiseTemplate)
                {
                    if (SubCatTemplate.Template != null)
                    {
                        var obj = new SCRMTemplateWithLayoutModel();

                        obj.Id = SubCatTemplate.Template.Id;
                        obj.Name = SubCatTemplate.Template.Name;
                        obj.TemplateImageURL = SubCatTemplate.Template.TemplateImageURL;
                        obj.IsActive = SubCatTemplate.Template.IsActive;
                        obj.IsFree = SubCatTemplate.Template.IsFree;

                        var templateLayout = await _templateLayoutRepository.GetTemplateLayoutByIdAsync(SubCatTemplate.Template.Id);

                        if (templateLayout != null)
                        {
                            obj.TextFields = templateLayout.TextFields;
                            obj.ImageFields = templateLayout.ImageFields;
                        }

                        templateModel.Add(obj);
                    }
                }
                objCat.TemplateAndLayout = templateModel;

                lstCategoryModel.Add(objCat);
            }

            return lstCategoryModel;
        }

        public async Task<SCRMCategoryWiseSubCategory> GetCategoryWiseSubCategory(int categoryId)
        {
            SCRMCategoryWiseSubCategory records = new SCRMCategoryWiseSubCategory();

            records.CategoryId = categoryId;
            //records.Data = await _context.tblSCRMSubCategory.Where(x => x.CategoryId == categoryId).Select(x => new SCRMSubCategoryModel()
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    EventDate = x.EventDate,
            //    IsActive = x.IsActive,
            //    CategoryId = x.CategoryId,
            //    CategoryName = x.Category.Name,
            //    SubCategoryImageURL = !string.IsNullOrWhiteSpace(x.SubCategoryImage) ? _baseRepository.GetBaseUrl() + x.SubCategoryImage : string.Empty
            //}).ToListAsync();


            records.Data = Query(x => x.Id == categoryId).Select(x => new SCRMSubCategoryModel()
            {
                Id = x.Id,
                Name = x.Name,
                EventDate = x.EventDate,
                IsActive = x.IsActive,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,
                SubCategoryImageURL = !string.IsNullOrWhiteSpace(x.SubCategoryImage) ? _imageRepository.GetImageBaseUrl() + x.SubCategoryImage : string.Empty
            }).ToList();

            return records;
        }
    }
}
