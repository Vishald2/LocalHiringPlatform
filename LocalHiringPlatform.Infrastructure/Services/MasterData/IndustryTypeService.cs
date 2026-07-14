using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Interfaces.MasterDataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Services.MasterData
{
    public class IndustryTypeService : IIndustryTypeService
    {
        private readonly IIndustryTypeRepository _industryTypeRepository;

        public IndustryTypeService(
            IIndustryTypeRepository industryTypeRepository)
        {
            _industryTypeRepository = industryTypeRepository;
        }

        public async Task<List<IndustryTypeResponseModel>> GetAllAsync()
        {
            var industries =
                await _industryTypeRepository.GetActiveAsync();

            return industries.Select(x => new IndustryTypeResponseModel
            {
                IndustryTypeId = x.IndustryTypeId,
                Name = x.Name
            }).ToList();
        }
    }
}
