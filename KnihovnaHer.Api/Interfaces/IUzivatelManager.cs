using KnihovnaHer.Dto;

namespace KnihovnaHer.Api.Interfaces
{
    public interface IUzivatelManager
    {
        Task<UzivatelDto?> AddUzivatelAsnyc(UzivatelCreateDto uzivatelDto);
        Task<UzivatelDto?> DeleteUzivatelAsync(string id);
        Task<IList<UzivatelDto>> GetAllUzivatelAsync();
        Task<UzivatelDto?> GetUzivatelByIdAsync(string id);
        Task<UzivatelDto?> UpdateUzivatelAsync(string id, UzivatelEditDto uzivatelDto);
    }
}
