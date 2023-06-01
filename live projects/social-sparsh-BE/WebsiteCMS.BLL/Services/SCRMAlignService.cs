using Microsoft.EntityFrameworkCore;
using NPOI.OpenXmlFormats.Dml.WordProcessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteCMS.BLL.Interfaces;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Models;

namespace WebsiteCMS.BLL.Services
{
    public class SCRMAlignService : ISCRMAlignService
    {
        private readonly ISCRMAlignRepository _alignRepository;

        public SCRMAlignService(ISCRMAlignRepository alignRepository)
        {
            _alignRepository = alignRepository;
        }

        public async Task<List<SCRMAlignModel>> GetAllAlignAsync()
        {
            List<SCRMAlignModel> record = new();

            var alignData = await _alignRepository.GetAllAlignAsync();

            if (alignData != null)
            {
                record = alignData
                       .Select(x => new SCRMAlignModel()
                       {
                           Id = x.Id,
                           Name = x.Name
                       }).OrderBy(x => x.Id).ToList();
            }
            return record;
        }

        public async Task<SCRMAlignModel> GetAlignByIdAsync(int id)
        {
            SCRMAlignModel record = null;

            var alignData = await _alignRepository.GetAlignByIdAsync(id);

            if (alignData != null)
            {
                record = PrepareAlignModel(alignData);
            }
            return record!;
        }

        public async Task<SCRMAlignModel> AddAlignAsync(SCRMAlignModel model)
        {
            SCRMAlignModel record = null;

            var data = new SCRMAlign()
            {
                Name = model.Name
            };

            var alignData = await _alignRepository.AddAlignAsync(data);

            if (alignData != null)
            {
                record = PrepareAlignModel(alignData);
            }
            return record!;
        }

        public async Task<SCRMAlignModel> UpdateAlignAsync(int id, SCRMAlignModel model)
        {
            SCRMAlignModel record = new();

            var align = await _alignRepository.GetAlignByIdAsync(id);

            if (align != null)
            {
                align.Name = model.Name;

                SCRMAlign alignData = new();
                alignData = await _alignRepository.UpdateAlignAsync(align);

                if (alignData != null)
                {
                    record = PrepareAlignModel(alignData);
                }
            }
            return record;
        }

        public async Task<bool> DeleteAlignAsync(int id)
        {
            return await _alignRepository.DeleteAlignAsync(id);
        }

        private SCRMAlignModel PrepareAlignModel(SCRMAlign model)
        {
            var alignModel = new SCRMAlignModel();

            if (model != null)
            {
                alignModel.Id = model.Id;
                alignModel.Name = model.Name;
            }

            return alignModel;
        }
    }
}