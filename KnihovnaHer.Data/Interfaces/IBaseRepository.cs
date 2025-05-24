using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaHer.Data.Interfaces
{
    public interface IBaseRepository <TEntity> where TEntity : class
    {
        IList<TEntity> GetAll ();

        TEntity? FindById (uint id);

        TEntity Insert (TEntity entity);

        TEntity Update (TEntity entity);

        void Delete (uint id);

        bool ExistsWithId(uint id);



    }
}
