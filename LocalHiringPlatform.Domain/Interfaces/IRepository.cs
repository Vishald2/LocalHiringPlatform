using LocalHiringPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        Task AddAsync(T entity);

        Task<T?> GetByIdAsync(Guid entityId);

        Task<List<T>>GetAllAsync();

        void Remove(T entity);
    }
}
