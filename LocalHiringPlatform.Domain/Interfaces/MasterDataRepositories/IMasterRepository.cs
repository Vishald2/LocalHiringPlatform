using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces.MasterDataRepositories
{
    public interface IMasterRepository<T>
        where T : class
    {
        Task<List<T>> GetAllAsync();

        Task AddAsync(T entity);

        void Remove(T entity);
    }
}
