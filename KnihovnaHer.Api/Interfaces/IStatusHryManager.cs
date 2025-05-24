using KnihovnaHer.Dto;

namespace KnihovnaHer.Api.Interfaces
{
    public interface IStatusHryManager
    {
        StatusHryViewDto AddStatusHry(StatusHryCreateDto statusHryDto, string userId);
        StatusHryViewDto? DeleteStatusHry(uint statusHryId);
        StatusHryViewDto? EditStatusHry(uint statusHryId, StatusHryEditDto statusHryEdit);
        Task<IList<StatusHryViewDto>> GetAllStatusForUser(string uzivatelId);
    }
}
