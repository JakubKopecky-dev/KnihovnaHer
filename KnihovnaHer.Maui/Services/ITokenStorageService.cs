using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaHer.Maui.Services
{
    public interface ITokenStorageService
    {
        void DeleteToken();
        Task<string?> GetTokenAsync();
        Task<bool> HasTokenAsync();
        Task<bool> IsAdminAsync();
        Task<bool> IsTokenValidAsync();
        Task SaveTokenAsync(string token);
    }
}
