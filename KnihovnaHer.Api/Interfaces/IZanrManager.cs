using KnihovnaHer.Dto;

namespace KnihovnaHer.Api.Interfaces
{
    public interface IZanrManager
    {
        Task<ZanrDto> AddZanrAsync(ZanrDto zanrDto);
        Task<ZanrDto?> DeleteZanrAsync(uint zanrId);
        Task<IList<ZanrDto>> GetAllZanrAsync();
        Task<ZanrDto?> GetZanrAsync(uint zanrId);
    }
}
