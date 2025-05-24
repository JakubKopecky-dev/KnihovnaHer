using AutoMapper;
using KnihovnaHer.Api.Interfaces;
using KnihovnaHer.Dto;
using KnihovnaHer.Data.Interfaces;
using KnihovnaHer.Data.Models;
using KnihovnaHer.Data.Repositories;

namespace KnihovnaHer.Api.Managers
{
    public class HraManager(IHraRepository hraRepository, IMapper mapper, IZanrRepository zanrRepository, IStatusHryRepository statusHryRepository) : IHraManager
    {
        private readonly IHraRepository hraRepository = hraRepository;
        private readonly IZanrRepository zanrRepository = zanrRepository;
        private readonly IStatusHryRepository statusHryRepository = statusHryRepository;
        private readonly IMapper mapper = mapper;


        public IList<HraDto> GetAllHra()
        {
            IList<Hra> hra = hraRepository.GetAll();

            return mapper.Map<IList<HraDto>>(hra);

        }

        public HraDto? GetHra(uint id)
        {

            Hra? hra = hraRepository.FindById(id);
            
            if(hra is null) 
                return null;

            return mapper.Map<HraDto>(hra);

        }

        public HraDto AddHra(HraCreateEditDto hraDto)
        {
            Hra hra = mapper.Map<Hra>(hraDto);
            hra.HraId = default;
            hra.Zanry.AddRange(zanrRepository.FindAllByNames(hraDto.Zanry));
            Hra addedHra = hraRepository.Insert(hra);


            Hra hraVratit = hraRepository.FindById(hra.HraId)!;
                
            

            return mapper.Map<HraDto>(hraVratit);

        }

        public HraDto? UpdateHra(uint hraId, HraCreateEditDto hraDto)
        {
        

           Hra? hraDb = hraRepository.FindById(hraId);
            if (hraDb is null)
                return null;

            mapper.Map<HraCreateEditDto,Hra>(hraDto, hraDb);
  
          

            IList<Zanr> zanry = zanrRepository.FindAllByNames(hraDto.Zanry);

            foreach (Zanr z in hraDb.Zanry.Except(zanry).ToList()) 
            {
                hraDb.Zanry.Remove(z);
            }

            // Přidat ty, které tam ještě nejsou
            foreach (Zanr z in zanry.Except(hraDb.Zanry).ToList()) 
            {
                hraDb.Zanry.Add(z);
            }


            Hra uppdatedHra = hraRepository.Update(hraDb);

            return mapper.Map<HraDto>(uppdatedHra);


        }


        public HraDto? DeleteHra(uint hraId)
        {
            if (!hraRepository.ExistsWithId(hraId))
                return null;

            Hra hra = hraRepository.FindById(hraId)!;
            HraDto hraDto = mapper.Map<HraDto>(hra);

            hra.Zanry.Clear();

            var statusyHer = statusHryRepository.FindByHraId(hraId);
            foreach(var stat  in statusyHer)
                statusHryRepository.Delete(stat.StatusHryId);

            hraRepository.Update(hra);
            hraRepository.Delete(hraId);

            return hraDto;

        }





    }
}
