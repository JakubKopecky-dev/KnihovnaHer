using KnihovnaHer.Dto;

namespace KnihovnaHer.Api.Interfaces
{
    public interface IHraManager
    {
        HraDto AddHra(HraCreateEditDto hraDto);
        HraDto? DeleteHra(uint hraId);
        IList<HraDto> GetAllHra();
        HraDto? GetHra(uint id);
        HraDto? UpdateHra(uint hraId, HraCreateEditDto hraDto);

    }
}
