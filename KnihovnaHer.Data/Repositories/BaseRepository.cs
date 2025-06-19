using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaHer.Data.Interfaces;
using KnihovnaHer.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KnihovnaHer.Data.Repositories
{
    public class BaseRepository<TEntity>(KnihovnaHerDbContext knihovnaHerDbContext) : IBaseRepository<TEntity> where TEntity : class
    {

        protected readonly KnihovnaHerDbContext knihovnaHerDbContex = knihovnaHerDbContext;
        protected readonly DbSet<TEntity> dbSet = knihovnaHerDbContext.Set<TEntity>();


        public async Task<TEntity?> FindByIdAsync(uint id) => await dbSet.FindAsync(id);

        public async Task<bool> ExistsWithIdAsync(uint id)
        {
            TEntity? entity =  await dbSet.FindAsync(id);

            if (entity is not null)
                knihovnaHerDbContex.Entry(entity).State = EntityState.Detached;

            return entity is not null;
        }


        public async Task<IList<TEntity>> GetAllAsync() => await dbSet.ToListAsync();

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
           EntityEntry<TEntity> entityEntry = await dbSet.AddAsync(entity);
           await knihovnaHerDbContex.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = dbSet.Update(entity);
           await knihovnaHerDbContex.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task DeleteAsync(uint id)
        {
            TEntity? entity = await dbSet.FindAsync(id);

            if (entity is null)
                return;

            try
            {
                dbSet.Remove(entity);
                await knihovnaHerDbContex.SaveChangesAsync();
            }
            catch
            {
                knihovnaHerDbContex.Entry(entity).State = EntityState.Unchanged;
                throw;
            }


        }













    }
}
