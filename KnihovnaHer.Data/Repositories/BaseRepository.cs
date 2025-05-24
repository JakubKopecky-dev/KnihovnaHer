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


        public TEntity? FindById(uint id) => dbSet.Find(id);

        public bool ExistsWithId(uint id)
        {
            TEntity? entity = dbSet.Find(id);

            if (entity is not null)
                knihovnaHerDbContex.Entry(entity).State = EntityState.Detached;

            return entity is not null;
        }


        public IList<TEntity> GetAll() => dbSet.ToList();

        public TEntity Insert(TEntity entity)
        {
           EntityEntry<TEntity> entityEntry = dbSet.Add(entity);
            knihovnaHerDbContex.SaveChanges();

            return entityEntry.Entity;
        }

        public TEntity Update(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = dbSet.Update(entity);
            knihovnaHerDbContex.SaveChanges();

            return entityEntry.Entity;
        }

        public void Delete(uint id)
        {
            TEntity? entity = dbSet.Find(id);

            if (entity is null)
                return;

            try
            {
                dbSet.Remove(entity);
                knihovnaHerDbContex.SaveChanges();
            }
            catch
            {
                knihovnaHerDbContex.Entry(entity).State = EntityState.Unchanged;
                throw;
            }


        }













    }
}
