using KnihovnaHer.Dto;

namespace KnihovnaHer.Api.Interfaces
{
    public interface IVydavatelManager
    {
        Task<VydavatelDto> AddVydavatelAsync(VydavatelDto vydavatelDto);
        Task<VydavatelDto?> DeleteVydavatelAsync(uint vydavatelId);
        Task<VydavatelDto?> EditVydavatelAsync(uint vydatavatelId, VydavatelDto vydavatelDto);
        Task<IList<VydavatelDto>> GetAllVydavatelAsync();
        Task<VydavatelDto?> GetVydavatelAsync(uint id);
    }
}
