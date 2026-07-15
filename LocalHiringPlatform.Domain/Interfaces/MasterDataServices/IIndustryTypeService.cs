using LocalHiringPlatform.Domain.Models.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces.MasterDataServices
{
    public interface IIndustryTypeService
    {
        Task<List<IndustryTypeResponseModel>> GetAllAsync();
    }
}
