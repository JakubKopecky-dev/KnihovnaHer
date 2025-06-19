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

        public async Task<IList<StatusHry>> FindByUzivatelIdAsync(string uzivatelId) => await dbSet.Where(a => a.UzivatelId == uzivatelId).ToListAsync();


        public async  Task<IList<StatusHry>> FindByHraIdAsync(uint hraId) => await dbSet.Where(a => a.HraId == hraId).ToListAsync();



        public async Task<StatusHry?> FindByIdWithIncludeAsync(uint statusHryId)
        {
            return await dbSet
                .Include(s => s.Hra)
                .Include(s => s.Uzivatel)
                .Where(s => s.StatusHryId == statusHryId).FirstOrDefaultAsync();
        }

    }
}
