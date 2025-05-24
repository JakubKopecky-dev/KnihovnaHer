using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaHer.Data.Interfaces;
using KnihovnaHer.Data.Models;

namespace KnihovnaHer.Data.Repositories
{
    public class ZanrRepository(KnihovnaHerDbContext knihovnaHerDbContext) : BaseRepository<Zanr>(knihovnaHerDbContext),IZanrRepository
    {
        public IList<Zanr> FindAllByNames(IEnumerable<string> names) => dbSet.Where(g=> names.Contains(g.Nazev)).ToList();

    }
}
