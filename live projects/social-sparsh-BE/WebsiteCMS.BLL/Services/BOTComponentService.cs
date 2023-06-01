using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Data.Repositories;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.BLL.Services
{
    public class BOTComponentService : IBOTComponentService
    {
        private readonly IBOTComponentRepository _componentRepository;
        private readonly IMapper _Mapper;
        private readonly IAWSImageService _imageService;

        public BOTComponentService(IBOTComponentRepository componentRepository, IMapper mapper, IAWSImageService imageService)
        {
            _componentRepository = componentRepository;
            _Mapper = mapper;
            _imageService = imageService;
        }

        public async Task<List<BOTComponentModel>?> GetAllComponentsAsync()
        {
            List<BOTComponentModel> record = new();
            var Component = await _componentRepository.GetAllComponentsAsync();

            if (Component != null)
            {
                Component.ForEach(x => x.IconUrl = Path.Combine(_imageService.GetImageBaseUrl(), x.IconUrl!));
                record = _Mapper.Map<List<BOTComponentModel>>(Component);
                return record;
            }
            return null;
        }

        public async Task<BOTComponentModel> AddComponentAsync(BOTComponentModel model)
        {
            BOTComponentModel record = null;
            if(model.Icon != null)
            {
                await using var stream = new MemoryStream();
                await model.Icon.CopyToAsync(stream);
                model.IconUrl = await _imageService.UploadFileAsync(stream, model.Icon.FileName, AWSDirectory.BOTComponent);
            }

            BOTComponent component = _Mapper.Map<BOTComponent>(model);

            var components = await _componentRepository.AddComponentAsync(component);

            if (components != null)
            {
                record = _Mapper.Map<BOTComponentModel>(components);
            }
            return record!;
        }

        public async Task<BOTComponentModel> EditComponentAsync(BOTComponentModel model)
        {
            BOTComponentModel record = new();
            if (model.Icon != null)
            {
                await using var stream = new MemoryStream();
                await model.Icon.CopyToAsync(stream);
                model.IconUrl = await _imageService.UploadFileAsync(stream, model.Icon.FileName, AWSDirectory.BOTComponent);
            }

            var component = _Mapper.Map<BOTComponent>(model);

            if (component != null)
            {
                BOTComponent components = new BOTComponent();
                components = await _componentRepository.EditComponentAsync(component);

                if (components != null)
                {
                    record = _Mapper.Map<BOTComponentModel>(components);
                }
            }
            return record;
        }

    }
}
