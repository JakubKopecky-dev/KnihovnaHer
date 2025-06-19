using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaHer.Data.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task DeleteAsync(uint id);
        Task<bool> ExistsWithIdAsync(uint id);
        Task<TEntity?> FindByIdAsync(uint id);
        Task<IList<TEntity>> GetAllAsync();
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
