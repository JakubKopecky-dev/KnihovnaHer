using KnihovnaHer.Dto;

namespace KnihovnaHer.Api.Interfaces
{
    public interface IZanrManager
    {
        ZanrDto AddZanr(ZanrDto zanrDto);
        ZanrDto? DeleteZanr(uint zanrId);
        IList<ZanrDto> GetAllZanr();
        ZanrDto? GetZanr(uint zanrId);
    }
}
