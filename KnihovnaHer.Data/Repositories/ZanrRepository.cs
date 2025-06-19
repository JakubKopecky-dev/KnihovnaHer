using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaHer.Data.Interfaces;
using KnihovnaHer.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KnihovnaHer.Data.Repositories
{
    public class ZanrRepository(KnihovnaHerDbContext knihovnaHerDbContext) : BaseRepository<Zanr>(knihovnaHerDbContext),IZanrRepository
    {
        public async Task<IList<Zanr>> FindAllByNamesAsync(IEnumerable<string> names) => await dbSet.Where(g => names.Contains(g.Nazev)).ToListAsync();

    }
}
