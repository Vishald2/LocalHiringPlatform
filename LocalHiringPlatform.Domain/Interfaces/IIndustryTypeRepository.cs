using LocalHiringPlatform.Domain.Entities.Experience;
using LocalHiringPlatform.Domain.Interfaces.MasterDataRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces
{
    public interface IIndustryTypeRepository
        : IMasterRepository<IndustryType>
    {
        Task<IndustryType?> GetByIdAsync(
            int industryTypeId);

        Task<List<IndustryType>> GetActiveAsync();
    }
}
