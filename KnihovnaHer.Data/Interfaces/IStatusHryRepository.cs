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
        IList<StatusHry> FindByHraId(uint hraId);
        StatusHry? FindByIdWithInclude(uint Id);
        IList<StatusHry> FindByUzivatelId(string uzivatelId);
    }
}
