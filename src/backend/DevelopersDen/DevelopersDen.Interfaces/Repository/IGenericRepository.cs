using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersDen.Interfaces.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity> GetByGuidAsync(Guid id);
        Task<List<TEntity?>> GetAllAsync(bool tracked = true);
        Task UpdateAsync(TEntity entity);
        Task DeleteByIdAsync(int id);
        Task SaveAsync();
    }
}
