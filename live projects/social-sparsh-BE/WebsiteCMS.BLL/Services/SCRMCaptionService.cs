using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.BLL.Services
{
    public class SCRMCaptionService : ISCRMCaptionService
    {
        private readonly ISCRMCaptionRepository _caption;
        private readonly IMapper _Mapper;

        public SCRMCaptionService(ISCRMCaptionRepository caption, IMapper mapper)
        {
            _caption = caption;
            _Mapper = mapper;
        }

        public async Task<List<SCRMCaptionsModel>?> GetAllCaptionAsync()
        {
            List<SCRMCaptionsModel> record = new();
            var Component = await _caption.GetAllCaptionAsync();

            if (Component != null)
            {
                record = _Mapper.Map<List<SCRMCaptionsModel>>(Component);
                return record;
            }
            return null;
        }      
        
        public async Task<SCRMCaptionsModel?> GetCaptionById(int id)
        {
            SCRMCaptionsModel record = new();
            var Component = await _caption.GetCaptionByIdAsync(id);

            if (Component != null)
            {
                record = _Mapper.Map<SCRMCaptionsModel>(Component);
                return record;
            }
            return null;
        }
        public async Task<List<SCRMCaptionsModel>?> GetCaptionByCategoryId(int id)
        {
            List<SCRMCaptionsModel> record = new();
            var Component = await _caption.GetCaptionByCategoryIdAsync(id);

            if (Component != null)
            {
                record = _Mapper.Map<List<SCRMCaptionsModel>>(Component);
                return record;
            }
            return null;
        }
        
        public async Task<List<SCRMCaptionsModel>?> GetCaptionBySubCategoryId(int id)
        {
            List<SCRMCaptionsModel> record = new();
            var Component = await _caption.GetCaptionBySubCategoryIdAsync(id);

            if (Component != null)
            {
                record = _Mapper.Map<List<SCRMCaptionsModel>>(Component);
                return record;
            }
            return null;
        }

        public async Task<SCRMCaptionsModel> AddCaptionAsync(SCRMCaptionsModel model)
        {
            SCRMCaptionsModel record = null;

            if (model.SCRMSubCategoryId == null && model.SCRMCategoryID == null)
                return record!;
            SCRMCaptions component = _Mapper.Map<SCRMCaptions>(model);

            var components = await _caption.AddCaptionAsync(component);

            if (components != null)
            {
                record = _Mapper.Map<SCRMCaptionsModel>(components);
            }
            return record!;
        }

        public async Task<SCRMCaptionsModel> EditCaptionAsync(SCRMCaptionsModel model)
        {
            SCRMCaptionsModel record = new();

            var component = _Mapper.Map<SCRMCaptions>(model);

            if (component != null)
            {
                SCRMCaptions components = new SCRMCaptions();
                components = await _caption.UpdateCaptionAsync(component);

                if (components != null)
                {
                    record = _Mapper.Map<SCRMCaptionsModel>(components);
                }
            }
            return record;
        }

        public async Task<long> DeleteCaptionAsync(int captionId)
        {
            await _caption.DeleteCaptionAsync(captionId);
            return captionId;
        }
    }
}
