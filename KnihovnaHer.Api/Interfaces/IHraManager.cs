using KnihovnaHer.Dto;

namespace KnihovnaHer.Api.Interfaces
{
    public interface IHraManager
    {
        Task<HraDto> AddHraAsync(HraCreateEditDto hraDto);
        Task<HraDto?> DeleteHra(uint hraId);
        Task<IList<HraDto>> GetAllHraAsync();
        Task<HraDto?> GetHraAsync(uint id);
        Task<HraDto?> UpdateHra(uint hraId, HraCreateEditDto hraDto);
    }
}
