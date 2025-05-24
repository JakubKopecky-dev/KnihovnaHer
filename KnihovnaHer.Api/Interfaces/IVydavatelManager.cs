using KnihovnaHer.Dto;

namespace KnihovnaHer.Api.Interfaces
{
    public interface IVydavatelManager
    {
        VydavatelDto AddVydavatel(VydavatelDto vydavatelDto);
        VydavatelDto? DeleteVydavatel(uint vydavatelId);
        VydavatelDto? EditVydavatel(uint vydatavatelId, VydavatelDto vydavatelDto);
        IList<VydavatelDto> GetAllVydavatel();
        VydavatelDto? GetVydavatel(uint id);
    }
}
