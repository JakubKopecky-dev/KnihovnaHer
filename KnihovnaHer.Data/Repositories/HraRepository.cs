using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaHer.Data.Interfaces;
using KnihovnaHer.Data.Models;

namespace KnihovnaHer.Data.Repositories
{
    public class HraRepository(KnihovnaHerDbContext knihovnaHerDbContext) : BaseRepository<Hra>(knihovnaHerDbContext),IHraRepository
    {
    }
}
