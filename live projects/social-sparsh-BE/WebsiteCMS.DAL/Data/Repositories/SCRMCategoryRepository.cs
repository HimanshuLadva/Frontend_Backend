using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.Record;
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
    public class SCRMCategoryRepository : Repository<SCRMCategory>, SCRMICategoryRepository
    {
        private readonly WebsiteCMSDbContext _context;
        private SCRMITemplateLayoutRepository _templateLayoutRepository;
        private readonly IBaseRepository _baseRepository;
        private readonly IAWSImageService _imageRepository;

        public SCRMCategoryRepository(WebsiteCMSDbContext context, SCRMITemplateLayoutRepository templateLayoutRepository, IBaseRepository baseRepository, IAWSImageService imageRepository) : base(context)
        {
            _context = context;
            _templateLayoutRepository = templateLayoutRepository;
            _baseRepository = baseRepository;
            _imageRepository = imageRepository;
        }

        public async Task<IPagedList<SCRMCategoryModel>> GetAllCategoryAsync(SCRMRequestParams requestParams)
        {
            var records = new List<SCRMCategoryModel>();
            records = await _context.tblSCRMCategory
                    .Select(x => new SCRMCategoryModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        EventDate = x.EventDate,
                        IsActive = x.IsActive,
                        CategoryImageUrl = !string.IsNullOrWhiteSpace(x.CategoryImage) ? _baseRepository.GetImageBaseUrl() + x.CategoryImage : string.Empty
                    }).Where(x => x.CategoryImageUrl != string.Empty).ToListAsync();

            if (requestParams.search != null)
                records = await records.Where(x => x.Name.ToLower().Contains(requestParams.search.ToLower())).ToListAsync();

            if (requestParams.isActive == "Active" || requestParams.isActive == "active")
                records = await records.Where(x => x.IsActive == true).ToListAsync();

            requestParams.recordCount = records.Count;

            var data = await SortResult<SCRMCategoryModel>(records, requestParams);
            return data;
        }

        public async Task<List<SCRMCategoryModel>> GetAllActiveCategoryAsync()
        {
            var records = new List<SCRMCategoryModel>();
            records = await _context.tblSCRMCategory
                .Where(x => x.IsActive == true)
                    .Select(x => new SCRMCategoryModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        EventDate = x.EventDate,
                        IsActive = x.IsActive,
                        CategoryImageUrl = !string.IsNullOrWhiteSpace(x.CategoryImage) ? _baseRepository.GetImageBaseUrl() + x.CategoryImage : string.Empty
                    }).ToListAsync();
            return records;
        }

        public async Task<SCRMCategoryModel> GetCategoryByIdAsync(int id)
        {
            var records = await _context.tblSCRMCategory
                .Where(x => x.Id == id)
                .Select(x => new SCRMCategoryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    EventDate = x.EventDate,
                    IsActive = x.IsActive,
                    CategoryImageUrl = !string.IsNullOrWhiteSpace(x.CategoryImage) ? _baseRepository.GetImageBaseUrl() + x.CategoryImage : string.Empty
                }).FirstOrDefaultAsync();
            return records!;
        }

        public async Task<SCRMCategoryModel> AddCategoryAsync(SCRMCategoryModel model)
        {
            await using var memoryStream = new MemoryStream();
            await model.CategoryImage!.CopyToAsync(memoryStream);
            string docName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(model.CategoryImage.FileName));
            string imageUrl = await _imageRepository.UploadFileAsync(memoryStream, docName, AWSDirectory.SCRMCategory);

            var data = new SCRMCategory()
            {
                Name = model.Name,
                EventDate = model.EventDate,
                CategoryImage = imageUrl
            };

            Insert(data);
            await SaveChangesAsync();

            var record = await GetCategoryByIdAsync(data.Id);
            return record;
        }

        public async Task<SCRMCategoryModel> UpdateCategoryAsync(int id, SCRMCategoryModel model)
        {
            var data = await GetByIDAsync(id);
            string imageUrl = "";
            string docName = "";
            await using var memoryStream = new MemoryStream();

            if (model.CategoryImage != null)
            {
                await model.CategoryImage!.CopyToAsync(memoryStream);
                docName = $"{Guid.NewGuid()}.{model.CategoryImage.FileName.Split(".").Last()}";
                bool isDeletedOrNot = await _imageRepository.DeleteImageAsync(data.CategoryImage!);
                imageUrl = await _imageRepository.UploadFileAsync(memoryStream, docName, AWSDirectory.SCRMCategory);
            }
            if (data != null)
            {
                data.Id = id;
                data.Name = model.Name;
                data.EventDate = model.EventDate;
                data.IsActive = model.IsActive;
                data.CreatedDate = data.CreatedDate;
                data.UpdatedDate = DateTime.Now;
                data.IsDeleted = data.IsDeleted;

                if (!string.IsNullOrEmpty(docName))
                {
                    data.CategoryImage = imageUrl;
                }

                _context.tblSCRMCategory.Update(data);
                await _context.SaveChangesAsync();

                var record = await GetCategoryByIdAsync(data.Id);
                return record;
            }
            return null!;
        }

        public async Task<bool> UpdateCategoryStatusAsync(int id, SCRMUpdateStatusModel model)
        {
            var data = await _context.tblSCRMCategory.FindAsync(id);
            if (data != null)
            {
                data.IsActive = model.IsActive;
                data.UpdatedDate = DateTime.Now;

                _context.tblSCRMCategory.Update(data);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var record = Query(x => x.Id == id).FirstOrDefault();
            bool isDeleted = await _imageRepository.DeleteImageAsync(record!.CategoryImage!);
            Delete(record!);

            //var existCategory = _context.tblSCRMTemplate.Where(x => x.CategoryId == id);
            //if (existCategory != null)
            //    foreach (var field in existCategory)
            //    {
            //        _context.tblSCRMTemplate.Remove(field);
            //    }

            await SaveChangesAsync();
        }

        public async Task<IPagedList<SCRMCategroyWiseTemplateModel>> GetAllCategoryWiseTemplateListAsync(SCRMRequestParams requestParams = null!)
        {
            var lstCategoryModel = new List<SCRMCategroyWiseTemplateModel>();
            var categories = await _context.tblSCRMCategory.Where(x => x.IsActive == true).ToListAsync();

            foreach (var item in categories)
            {
                var objCat = new SCRMCategroyWiseTemplateModel();
                var templateModel = new List<SCRMTemplateWithLayoutModel>();

                objCat.Id = item.Id;
                objCat.Name = item.Name;

                var catWiseTemplate = await _context.tblSCRMTemplateCategory.Include(x => x.Template).ThenInclude(x => x.Category).Where(x => x.CategoryId == item.Id).ToListAsync();
                foreach (var catTemplate in catWiseTemplate)
                {
                    if (catTemplate.Template != null)
                    {
                        var obj = new SCRMTemplateWithLayoutModel();

                        obj.Id = catTemplate.Template.Id;
                        obj.Name = catTemplate.Template.Name;
                        obj.TemplateImageURL = catTemplate.Template.TemplateImageURL;
                        obj.IsActive = catTemplate.Template.IsActive;
                        obj.IsFree = catTemplate.Template.IsFree;

                        var templateLayout = await _templateLayoutRepository.GetTemplateLayoutByIdAsync(catTemplate.Template.Id);

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
            if (requestParams != null)
            {
                if (requestParams.search != null)
                    lstCategoryModel = await lstCategoryModel.Where(x => x.Name.ToLower().Contains(requestParams.search.ToLower())).ToListAsync();

                requestParams.recordCount = lstCategoryModel.Count;

                var data = await SortResult<SCRMCategroyWiseTemplateModel>(lstCategoryModel, requestParams);
                return data;
            }
            else
            {
                var data = lstCategoryModel.ToPagedList();
                return data;
            }
        }

        public async Task<List<SCRMCategroyWiseTemplateModel>> GetCategoryWiseTemplateListAsync(int id)
        {
            var lstCategoryModel = new List<SCRMCategroyWiseTemplateModel>();
            //var category = await _context.tblSCRMCategory.ToListAsync().Result.Select(x => x.IsActive == true && x.Id == id);
            //var category = await _context.tblSCRMCategory.FindAsync(id);
            var category = await GetByIDAsync(id);


            var objCat = new SCRMCategroyWiseTemplateModel();
            var templateModel = new List<SCRMTemplateWithLayoutModel>();

            objCat.Id = category!.Id;
            objCat.Name = category.Name;

            var catWiseTemplate = await _context.tblSCRMTemplateCategory.Include(x => x.Template).ThenInclude(x => x.Category).Where(x => x.CategoryId == category.Id).ToListAsync();
            foreach (var catTemplate in catWiseTemplate)
            {
                if (catTemplate.Template != null)
                {
                    var obj = new SCRMTemplateWithLayoutModel();

                    obj.Id = catTemplate.Template.Id;
                    obj.Name = catTemplate.Template.Name;
                    obj.TemplateImageURL = catTemplate.Template.TemplateImageURL;
                    obj.IsActive = catTemplate.Template.IsActive;
                    obj.IsFree = catTemplate.Template.IsFree;

                    var templateLayout = await _templateLayoutRepository.GetTemplateLayoutByIdAsync(catTemplate.Template.Id);

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

            return lstCategoryModel;
        }

        public async Task<List<SCRMMultipleCategoryWiseTemplateModel>> GetAllMultipleCategoryWiseTemplateListAsync(SCRMMultipleCategorys categorys)
        {
            var categories = categorys.CategoryNames.ToLower().Split(',');
            var listOfMultipleCategoryWiseTemplateList = new List<SCRMMultipleCategoryWiseTemplateModel>();

            var categoryWiseTemplates = await GetAllCategoryWiseTemplateListAsync();

            foreach (var item in categoryWiseTemplates)
            {
                if (categories.Contains(item.Name.ToLower()))
                {
                    listOfMultipleCategoryWiseTemplateList.Add(new SCRMMultipleCategoryWiseTemplateModel()
                    {
                        category_id = item.Id,
                        category_name = item.Name,
                        TemplateList = item.TemplateAndLayout
                    });
                }
            }

            return listOfMultipleCategoryWiseTemplateList;

        }

        public static async Task<IPagedList<T>> SortResult<T>(List<T> source, SCRMRequestParams requestParams)
        {
            IOrderedEnumerable<T> data = source.OrderBy(s => s.GetType().GetProperty("Name"));
            if (requestParams.sortBy != null)
                if (requestParams.orderBy!.ToLower() == "desc")
                    data = source.OrderByDescending(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));
                else
                    data = source.OrderBy(s => s.GetType().GetProperty(requestParams.sortBy)!.GetValue(s));

            return await data.ToPagedListAsync(requestParams.pageNumber, requestParams.pageSize);
        }

    }
}
