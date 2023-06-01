using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.DAL.RequestModel.WCMSRequestModel;

namespace WebsiteCMS.BLL.Services
{
    public class WCMSProductFieldService : IWCMSProductFieldService
    {
        private readonly IWCMSProductFieldsRepository _producctFieldRepository;
        private readonly IMapper _mapper;

        public WCMSProductFieldService(IWCMSProductFieldsRepository producctFieldRepository, IMapper mapper)
        {
            _producctFieldRepository = producctFieldRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<List<WCMSProductFieldsViewModel>> GetAllProductFieldsAsync()
        {
            try
            {
                var fields = await _producctFieldRepository.GetAllProductFieldsAsync();
                var result = _mapper.Map<List<WCMSProductFieldsViewModel>>(fields);
                return result ?? new List<WCMSProductFieldsViewModel>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <inheritdoc/>
        public async Task<bool> AddProductFieldsAsync(List<WCMSProductFieldsModel> models)
        {
            try
            {
                var fields = _mapper.Map<List<WCMSProductFields>>(models);
                var result = await _producctFieldRepository.AddProductFieldsAsync(fields);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <inheritdoc/>
        public async Task<WCMSProductFieldsModel> UpdateProductFieldsAsync(WCMSProductFieldsModel model)
        {
            try
            {
                var fields = _mapper.Map<WCMSProductFields>(model);
                var record = await _producctFieldRepository.UpdateProductFieldAsync(fields);
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <inheritdoc/>
        public async Task<bool> DeleteProductFieldByIdAsync(int id)
        {
            try
            {
                var result = await _producctFieldRepository.DeleteProductFieldAsync(id);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}