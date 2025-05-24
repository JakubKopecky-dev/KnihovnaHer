
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



        public async Task<IList<StatusHryViewDto>> GetAllStatusForUser (string uzivatelId)
        {
            Uzivatel? uzivatel = await userManager.FindByIdAsync(uzivatelId);
            if (uzivatel is null)
                return [];


           IList<StatusHry> statusHry = statusHryRepository.FindByUzivatelId(uzivatelId);

            return mapper.Map<IList<StatusHryViewDto>>(statusHry);
        }

        public StatusHryViewDto AddStatusHry(StatusHryCreateDto statusHryDto,string userId)
        {
            StatusHry statusHry = mapper.Map<StatusHry>(statusHryDto);
            statusHry.StatusHryId = default;
            statusHry.Stav = StavHry.Nova;
            statusHry.UzivatelId = userId;
            StatusHry addedStatus =  statusHryRepository.Insert(statusHry);

            StatusHry withIncludes = statusHryRepository.FindByIdWithInclude(addedStatus.StatusHryId)!;
       

            return mapper.Map<StatusHryViewDto>(withIncludes);

        }

        public StatusHryViewDto? EditStatusHry(uint statusHryId,StatusHryEditDto statusHryEdit)
        {
            StatusHry? statusHryDb = statusHryRepository.FindById(statusHryId);
            if(statusHryDb is null) 
                return null;

            mapper.Map<StatusHryEditDto, StatusHry>(statusHryEdit, statusHryDb);

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

            StatusHry updatedStusHry = statusHryRepository.Update(statusHryDb);

            return mapper.Map<StatusHryViewDto>(updatedStusHry);



        }

        public StatusHryViewDto? DeleteStatusHry(uint statusHryId)
        {
           if(!statusHryRepository.ExistsWithId(statusHryId))
                return null;

           StatusHry dbStatusHry = statusHryRepository.FindById(statusHryId)!;
            StatusHryViewDto deletedStatusHry = mapper.Map<StatusHryViewDto>(dbStatusHry);

            statusHryRepository.Delete(statusHryId);
            return deletedStatusHry;
           


        }


















    }
}
