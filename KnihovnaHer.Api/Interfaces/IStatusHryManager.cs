using KnihovnaHer.Dto;

namespace KnihovnaHer.Api.Interfaces
{
    public interface IStatusHryManager
    {
        Task<StatusHryViewDto> AddStatusHryAsync(StatusHryCreateDto statusHryDto, string userId);
        Task<StatusHryViewDto?> UpdateStatusHryAsync(uint statusHryId, StatusHryUpdateDto statusHryEdit);
        Task<IList<StatusHryViewDto>> GetAllStatusForUserAsync(string uzivatelId);
        Task<StatusHryViewDto?> DeleteStatusHry(uint statusHryId);
    }
}
