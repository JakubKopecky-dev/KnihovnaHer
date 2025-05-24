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
    public class StatusHryRepository(KnihovnaHerDbContext knihovnaHerDbContext) : BaseRepository<StatusHry>(knihovnaHerDbContext), IStatusHryRepository
    {

        public IList<StatusHry> FindByUzivatelId(string uzivatelId)
        {
            return dbSet.Where(a => a.UzivatelId == uzivatelId).ToList();   

        }

        public IList<StatusHry> FindByHraId(uint hraId)
        {
            return dbSet.Where(a => a.HraId == hraId).ToList();
        }

        public StatusHry? FindByIdWithInclude(uint statusHryId)
        {
            return dbSet
                .Include(s => s.Hra)
                .Include(s => s.Uzivatel)
                .Where(s => s.StatusHryId == statusHryId).FirstOrDefault();
        }

    }
}
