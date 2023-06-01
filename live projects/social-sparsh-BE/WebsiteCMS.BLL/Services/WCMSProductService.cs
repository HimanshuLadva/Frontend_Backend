using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Data.Repositories;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.WCMSRequestModel;
using WebsiteCMS.Global.Configurations;

namespace WebsiteCMS.BLL.Services
{
    public class WCMSProductService : IWCMSProductService
    {
        private readonly IWCMSProductRepository _ProcuctRepository;
        private readonly IWCMSProductFieldsRepository _ProcuctFieldsRepository;
        private readonly IMapper _Mapper;
        private readonly IWCMSUserProductFieldRepository _UserProductFieldRepository;
        private readonly IAWSImageService _imageService;

        public WCMSProductService(IWCMSProductRepository productRepository,
                                  IMapper mapper,
                                  IWCMSProductFieldsRepository procuctFieldsRepository,
                                  IWCMSUserProductFieldRepository userProductFieldRepository,
                                  IAWSImageService imageService)
        {
            _ProcuctRepository = productRepository;
            _Mapper = mapper;
            _ProcuctFieldsRepository = procuctFieldsRepository;
            _UserProductFieldRepository = userProductFieldRepository;
            _imageService = imageService;
        }
        /// <inheritdoc/>
        public async Task<List<WCMSCategoryWiseProductsModel>> GetAllProductsByCategoryAsync(int CategoryId)
        {
            
            try
            {
                var products = await _ProcuctRepository.GetAllProductsByCategoryAsync(CategoryId);
                var url = Path.Combine(_imageService.GetImageBaseUrl(), _imageService.GetAWSDirectory(AWSDirectory.WCMSUserImages));
                products!.Select(x => x.Fields.Where(x => x.ProductFields.FieldType.Type.ToLower() == "file"))
                                      .ToList()
                                      .ForEach(x => x.ToList()
                                                     .ForEach(x => x.FieldValue = url + "/" + Path.GetFileName(x.FieldValue)));
                var result = _Mapper.Map<List<WCMSCategoryWiseProductsModel>>(products);
                return result ?? new List<WCMSCategoryWiseProductsModel>();
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        /// <inheritdoc/>
        public async Task<WCMSCategoryWiseProductsModel> GetProductByIdAsync(int id)
        {
            try
            {
                var fieldTypes = await _ProcuctFieldsRepository.GetAllProductFieldsAsync();
                var product = await _ProcuctRepository.GetProductByIdAsync(id);
                var url = Path.Combine(_imageService.GetImageBaseUrl(), _imageService.GetAWSDirectory(AWSDirectory.WCMSUserImages));
                product.Fields.Where(x => x.ProductFields.FieldType.Type.ToLower() == "file").ToList()
                                      .ForEach(x => x.FieldValue = url + "/" + Path.GetFileName(x.FieldValue));
                var result = _Mapper.Map<WCMSCategoryWiseProductsModel>(product);
                foreach (var item in fieldTypes.Except(product.Fields.Select(x => x.ProductFields)))
                {
                    var field = new WCMSUserProductFieldsModel()
                    {
                        FieldValue = string.Empty,
                        IsBannerField = false,
                        ProductFieldsId = item.Id,
                        Type = item.FieldType.Type,
                        FieldName = item.Name,
                        ProductsId = id,
                    };
                    result.Fields.Add(field);
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <inheritdoc/>
        public async Task<WCMSCategoryWiseProductsModel> AddProductAsync(WCMSCategoryWiseProductsModel model)
        {
            
            try
            {
                var files = model.files;
                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    var destination = _imageService.GetImageBaseUrl() + _imageService.GetAWSDirectory(AWSDirectory.WCMSUserImages) + "/";
                    if (!await _imageService.IsS3FileExists(Path.Combine(destination, fileName!), ""))
                    {
                        await _imageService.CopyObjectAsync(AWSDirectory.WCMSUploadedImages, AWSDirectory.WCMSUserImages, fileName);
                    }
                }
                var product = _Mapper.Map<WCMSCategoryWiseProducts>(model);
                var productData = await _ProcuctRepository.AddProductAsync(product);
                var result = _Mapper.Map<WCMSCategoryWiseProductsModel>(productData);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
            

        }
        /// <inheritdoc/>
        public async Task<WCMSCategoryWiseProductsModel> UpdateProductAsync(WCMSCategoryWiseProductsModel model)
        {
            
            try
            {
                var sourcePath = DirectoryConfig.Get(AppDirectory.WCMSUploadImgs);
                var DestinationPath = DirectoryConfig.Get(AppDirectory.WCMSUserImgs);
                List<WCMSUserProductFields> fields = await _UserProductFieldRepository.GetAllUserFieldsAsync(model.Id);

                var abcd = model.Fields.Select(x => x.Id);
                var deletedfields = fields.Select(x => x.Id).Except(abcd).ToList();
                foreach (var id in deletedfields)
                {
                    var field = model.Fields.Where(x => x.Id == id).First();
                    if (field.Type == "file")
                    {
                        var FileName = Path.GetFileName(field.FieldValue);
                        if (!await _imageService.IsS3FileExists(_imageService.GetAWSDirectory(AWSDirectory.WCMSUserImages) + "/" + FileName, ""))
                        {
                            await _imageService.DeleteImageAsync(_imageService.GetAWSDirectory(AWSDirectory.WCMSUserImages) + "/" + FileName);
                        }
                    }
                    await _UserProductFieldRepository.DeleteUserProductFieldAsync(id);
                }
                foreach (var file in model.files)
                {
                    var fileName = Path.GetFileName(file);
                    var destination = _imageService.GetImageBaseUrl() + _imageService.GetAWSDirectory(AWSDirectory.WCMSUserImages) + "/";
                    if (!await _imageService.IsS3FileExists(Path.Combine(destination, fileName!), ""))
                    {
                        await _imageService.CopyObjectAsync(AWSDirectory.WCMSUploadedImages, AWSDirectory.WCMSUserImages, fileName);
                    }
                }
                var FileFields = _ProcuctFieldsRepository.GetAllProductFieldsAsync().Result.Where(x => x.FieldType.Type == "file").Select(x => x.Id);

                model.Fields.Where(x => FileFields.Contains(x.ProductFieldsId)).ToList().ForEach(x =>
                {
                    x.FieldValue = "images/" + Path.GetFileName(x.FieldValue);
                });
                var product = _Mapper.Map<WCMSCategoryWiseProducts>(model);
                var productData = await _ProcuctRepository.UpdateProductAsync(product);
                var result = _Mapper.Map<WCMSCategoryWiseProductsModel>(productData);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <inheritdoc/>
        public async Task<bool> DeleteProductByIdAsync(int id)
        {
            
            try
            {
                var fields = await _UserProductFieldRepository.GetAllUserFieldsAsync(id);
                foreach (var item in fields.Where(x => x.ProductFields.FieldType.Type.ToLower() == "file"))
                {
                    var FileName = Path.GetFileName(item.FieldValue);
                    if (await _imageService.IsS3FileExists(_imageService.GetAWSDirectory(AWSDirectory.WCMSUserImages) + "/" + FileName, ""))
                    {
                        await _imageService.DeleteImageAsync(_imageService.GetAWSDirectory(AWSDirectory.WCMSUserImages) + "/" + FileName);
                    }
                }
                var result = await _ProcuctRepository.DeleteProductByIdAsync(id);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}