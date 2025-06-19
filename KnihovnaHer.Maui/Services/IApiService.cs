using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnihovnaHer.Dto;

namespace KnihovnaHer.Maui.Services
{
    public interface IApiService
    {
        Task<HraDto?> AddHraAsync(HraCreateEditDto hraCreateDto);
        Task<StatusHryViewDto?> AddStatusHryAsync(StatusHryCreateDto statusHryCreateDto);
        Task<UzivatelDto?> AddUzivatelAsync(UzivatelCreateDto uzivatelCreateDto);
        Task<VydavatelDto?> AddVydavatelAsync(VydavatelDto vydavatelDto);
        Task<ZanrDto?> AddZanrAsync(ZanrDto zanrDto);
        Task<HraDto?> DeleteHraAsync(uint hraId);
        Task<StatusHryViewDto?> DeleteStatusHryAsync(uint statusHryId);
        Task<UzivatelDto?> DeleteUzivatelAsync(string uzivatelId);
        Task<VydavatelDto?> DeleteVydavatel(uint vydavatelId);
        Task<ZanrDto?> DeleteZanr(uint zanrId);
        Task<List<UzivatelDto>> GetAllUzivatelAsync();
        Task<HraDto?> GetHra(uint hraId);
        Task<List<HraDto>> GetHryAsync();
        Task<List<StatusHryViewDto>> GetStatusHryByUserAsync();
        Task<UzivatelDto?> GetUzivatelAsync(string uzivatelId);
        Task<List<VydavatelDto>> GetVydavateleAsync();
        Task<List<ZanrDto>> GetZanryAsync();
        Task<AuthResponseDto?> LoginAsync(AuthDto authDto);
        Task<AuthResponseDto?> RegisterAsync(AuthDto authDto);
        Task<HraDto?> UpdateHraAsync(uint hraId, HraCreateEditDto hraEditDto);
        Task<StatusHryViewDto?> UpdateStatusHryAsync(uint statusHryId, StatusHryUpdateDto statusHryEditDto);
        Task<UzivatelDto?> UpdateUzivatel(string uzivatelId, UzivatelEditDto uzivatelEditDto);
    }
}
