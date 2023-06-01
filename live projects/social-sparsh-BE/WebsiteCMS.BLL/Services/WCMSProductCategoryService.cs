using AutoMapper;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Data.Repositories;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.Global.Configurations;

namespace WebsiteCMS.BLL.Services
{
    public class WCMSProductCategoryService : IWCMSProductCategoryService
    {
        private readonly IWCMSProductCategoryRepository _categoryRepository;
        private readonly IBaseRepository _baseRepository;
        private readonly IMapper _mapper;
        private readonly IAWSImageService _imageService;
        public WCMSProductCategoryService(IWCMSProductCategoryRepository categoryRepository,
                                          IMapper mapper,
                                          IBaseRepository baseRepository,
                                          IAWSImageService imageService)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _baseRepository = baseRepository;
            _imageService = imageService;
        }

        /// <inheritdoc/>
        public async Task<List<WCMSProductCategoriesModel>> GetAllCategoriesAsync()
        {
            try
            {
                var path = DirectoryConfig.Get(AppDirectory.WCMSUserImgs);
                var UserId = _baseRepository.GetUserId();
                var models = await _categoryRepository.GetAllCategoriesByUserAsync(UserId);

                var url = Path.Combine(_imageService.GetImageBaseUrl(), _imageService.GetAWSDirectory(AWSDirectory.WCMSUserImages));

                models!.ForEach(x => x.Products!.Select(x => x.Fields.Where(x => x.ProductFields.FieldType.Type.ToLower() == "file"))
                                      .ToList()
                                      .ForEach(x => x.ToList()
                                                     .ForEach(x => x.FieldValue = url + "/" + Path.GetFileName(x.FieldValue))));

                var result = _mapper.Map<List<WCMSProductCategoriesModel>>(models);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <inheritdoc/>
        public async Task<WCMSProductCategoriesModel> GetCategoryByIdAsync(int id)
        {

            
            try
            {
                var path = DirectoryConfig.Get(AppDirectory.WCMSUserImgs);
                var url = Path.Combine(_imageService.GetImageBaseUrl(), _imageService.GetAWSDirectory(AWSDirectory.WCMSUserImages));
                var category = await _categoryRepository.GetCategoryByIdAsync(id);
                if (category != null)
                {
                    category.Products!.Select(x => x.Fields.Where(x => x.ProductFields.FieldType.Type.ToLower() == "file"))
                                      .ToList()
                                      .ForEach(x => x.ToList()
                                                     .ForEach(x => x.FieldValue = url + "/" + Path.GetFileName(x.FieldValue)));
                }
                return _mapper.Map<WCMSProductCategoriesModel>(category);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <inheritdoc/>
        public async Task<WCMSProductCategoriesModel> AddCategoriesAsync(WCMSProductCategoriesModel model)
        {
            
            try
            {
                var category = _mapper.Map<WCMSProductCategories>(model);
                category.ApplicationUserId = _baseRepository.GetUserId();
                var entry = await _categoryRepository.AddCategoryAsync(category);
                var data = _mapper.Map<WCMSProductCategoriesModel>(entry);
                return data;
            }
            catch (Exception)
            {
               throw;
            }
            

        }
        /// <inheritdoc/>
        public async Task<WCMSProductCategoriesModel> UpdateCategoryAsync(WCMSProductCategoriesModel model)
        {
            try
            {
                var record = new WCMSProductCategoriesModel();
                var category = _mapper.Map<WCMSProductCategories>(model);
                var productData = await _categoryRepository.UpdateCategoryAsync(category);

                if (productData != null)
                {
                    record = _mapper.Map<WCMSProductCategoriesModel>(productData);
                }

                return record;
            }
            catch (Exception)
            {
               throw;
            }
        }
        /// <inheritdoc/>
        public async Task<bool> DeleteCategoryByIdAsync(int id)
        {
            
            try
            {
                var fields = (await _categoryRepository.GetCategoryByIdAsync(id))!.Products!.Select(x => x.Fields.Where(x => x.ProductFields.FieldType.Type.ToLower() == "file")).Aggregate((x, y) => x.Concat(y));
                if (fields != null || fields!.Count() > 0)
                {
                    foreach (var item in fields)
                    {
                        var filename = Path.GetFileName(item.FieldValue);
                        if (await _imageService.IsS3FileExists(_imageService.GetAWSDirectory(AWSDirectory.WCMSUserImages) + "/" + filename))
                        {
                            await _imageService.DeleteImageAsync(_imageService.GetAWSDirectory(AWSDirectory.WCMSUserImages) + "/" + filename);
                        }
                    }
                }

                var result = await _categoryRepository.DeleteCategoryByIdAsync(id);
                return result;
            }
            catch (Exception)
            {
               throw;
            }
            
        }
    }
}