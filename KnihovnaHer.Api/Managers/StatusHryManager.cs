
using AutoMapper;
using KnihovnaHer.Api.Interfaces;
using KnihovnaHer.Dto;
using KnihovnaHer.Data.Interfaces;
using KnihovnaHer.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace KnihovnaHer.Api.Managers
{
    public class StatusHryManager (IStatusHryRepository statusHryRepository, UserManager<Uzivatel> userManager, IMapper mapper) : IStatusHryManager
    {
        private readonly IStatusHryRepository statusHryRepository = statusHryRepository;
        private readonly UserManager<Uzivatel> userManager = userManager;
        private readonly IMapper mapper = mapper;



        public async Task<IList<StatusHryViewDto>> GetAllStatusForUserAsync (string uzivatelId)
        {
            Uzivatel? uzivatel = await userManager.FindByIdAsync(uzivatelId);
            if (uzivatel is null)
                return [];


           IList<StatusHry> statusHry = await statusHryRepository.FindByUzivatelIdAsync(uzivatelId);

            return mapper.Map<IList<StatusHryViewDto>>(statusHry);
        }

        public async Task<StatusHryViewDto> AddStatusHryAsync(StatusHryCreateDto statusHryDto,string userId)
        {
            StatusHry statusHry = mapper.Map<StatusHry>(statusHryDto);
            statusHry.StatusHryId = default;
            statusHry.Stav = StavHry.Nova;
            statusHry.UzivatelId = userId;
            StatusHry addedStatus = await statusHryRepository.InsertAsync(statusHry);

            StatusHry withIncludes = (await statusHryRepository.FindByIdWithIncludeAsync(addedStatus.StatusHryId))!;
       

            return mapper.Map<StatusHryViewDto>(withIncludes);

        }

        public async Task<StatusHryViewDto?> UpdateStatusHryAsync(uint statusHryId,StatusHryUpdateDto statusHryEdit)
        {
            StatusHry? statusHryDb = await statusHryRepository.FindByIdAsync(statusHryId);
            if(statusHryDb is null) 
                return null;

            mapper.Map<StatusHryUpdateDto, StatusHry>(statusHryEdit, statusHryDb);

            switch (statusHryDb.Stav)
            {
                case StavHry.Nova:
                    statusHryDb.DatumZacatku = null;
                    statusHryDb.DatumDokonceni = null;
                    break;

                case StavHry.Hraji:
                    statusHryDb.DatumZacatku = DateTime.UtcNow;
                    statusHryDb.DatumDokonceni = null;
                    break;

                case StavHry.Dohrano:
                    statusHryDb.DatumDokonceni = DateTime.UtcNow;
                    break;
            }

            StatusHry updatedStusHry = await statusHryRepository.UpdateAsync(statusHryDb);

            return mapper.Map<StatusHryViewDto>(updatedStusHry);



        }

        public async Task<StatusHryViewDto?> DeleteStatusHry(uint statusHryId)
        {
           if(!await statusHryRepository.ExistsWithIdAsync(statusHryId))
                return null;

            StatusHry dbStatusHry = (await statusHryRepository.FindByIdAsync(statusHryId))!;
            StatusHryViewDto deletedStatusHry = mapper.Map<StatusHryViewDto>(dbStatusHry);

            await statusHryRepository.DeleteAsync(statusHryId);
            return deletedStatusHry;
           


        }


















    }
}
