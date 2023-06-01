using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Exceptions;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.BLL.Services
{
    public class BOTAvatarService : IBOTAvatarService
    {
        private readonly IBOTAvatarRepository _avatarRepository;
        private readonly IMapper _Mapper;

        public BOTAvatarService(IBOTAvatarRepository avatarRepository, IMapper mapper)
        {
            _avatarRepository = avatarRepository;
            _Mapper = mapper;
        }

        public async Task<List<BOTAvatarModel>?> getAvatar()
        {
            List<BOTAvatarModel> record = new();
            var avatarData = await _avatarRepository.getAvatar();

            if (avatarData != null)
            {
                record = _Mapper.Map<List<BOTAvatarModel>>(avatarData);
                return record;
            }
            return null;
        }

        public async Task<BOTAvatarModel> AddAvatarAsync(BOTAvatarModel model)
        {
            BOTAvatarModel record = null;

            BOTAvatar bot = _Mapper.Map<BOTAvatar>(model);

            var bots = await _avatarRepository.AddAvatarAsync(bot);

            if (bots != null)
            {
                record = _Mapper.Map<BOTAvatarModel>(bots);
            }
            return record!;
        }

        public async Task<bool?> DeleteAvatar(long id)
        {
            return await _avatarRepository.DeleteAvatarAsync(id);
        }

    }
}
