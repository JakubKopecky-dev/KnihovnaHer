using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaHer.Data.Models;

namespace KnihovnaHer.Data.Interfaces
{
    public interface IZanrRepository : IBaseRepository<Zanr>
    {
        Task<IList<Zanr>> FindAllByNamesAsync(IEnumerable<string> names);
    }
}
