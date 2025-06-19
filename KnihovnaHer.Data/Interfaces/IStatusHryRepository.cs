using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaHer.Data.Models;

namespace KnihovnaHer.Data.Interfaces
{
    public interface IStatusHryRepository : IBaseRepository<StatusHry>
    {
        Task<IList<StatusHry>> FindByHraIdAsync(uint hraId);
        Task<StatusHry?> FindByIdWithIncludeAsync(uint statusHryId);
        Task<IList<StatusHry>> FindByUzivatelIdAsync(string uzivatelId);
    }
}
