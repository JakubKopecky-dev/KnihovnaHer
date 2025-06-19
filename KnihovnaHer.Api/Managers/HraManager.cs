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


        public async Task<IList<HraDto>> GetAllHraAsync()
        {
            IList<Hra> hra = await hraRepository.GetAllAsync();

            return mapper.Map<IList<HraDto>>(hra);

        }

        public async Task<HraDto?> GetHraAsync(uint id)
        {

            Hra? hra = await hraRepository.FindByIdAsync(id);
            
            if(hra is null) 
                return null;

            return mapper.Map<HraDto>(hra);

        }

        public async Task<HraDto> AddHraAsync(HraCreateEditDto hraDto)
        {
            Hra hra = mapper.Map<Hra>(hraDto);
            hra.HraId = default;
            hra.Zanry.AddRange(await zanrRepository.FindAllByNamesAsync(hraDto.Zanry));
            await hraRepository.InsertAsync(hra);


            Hra hraVratit = (await hraRepository.FindByIdAsync(hra.HraId))!;
                
            

            return mapper.Map<HraDto>(hraVratit);

        }

        public async Task<HraDto?> UpdateHra(uint hraId, HraCreateEditDto hraDto)
        {
        

           Hra? hraDb = await hraRepository.FindByIdAsync(hraId);
            if (hraDb is null)
                return null;

            mapper.Map<HraCreateEditDto,Hra>(hraDto, hraDb);
  
          

            IList<Zanr> zanry = await zanrRepository.FindAllByNamesAsync(hraDto.Zanry);

            foreach (Zanr z in hraDb.Zanry.Except(zanry).ToList()) 
            {
                hraDb.Zanry.Remove(z);
            }

            // Přidat ty, které tam ještě nejsou
            foreach (Zanr z in zanry.Except(hraDb.Zanry).ToList()) 
            {
                hraDb.Zanry.Add(z);
            }


            Hra uppdatedHra = await hraRepository.UpdateAsync(hraDb);

            return mapper.Map<HraDto>(uppdatedHra);


        }


        public async Task<HraDto?> DeleteHra(uint hraId)
        {
            if (!(await hraRepository.ExistsWithIdAsync(hraId)))
                return null;

            Hra hra = (await hraRepository.FindByIdAsync(hraId))!;
            HraDto hraDto = mapper.Map<HraDto>(hra);

            hra.Zanry.Clear();

            var statusyHer = await statusHryRepository.FindByHraIdAsync(hraId);
            foreach(var stat  in statusyHer)
              await statusHryRepository.DeleteAsync(stat.StatusHryId);

           await hraRepository.UpdateAsync(hra);
           await hraRepository.DeleteAsync(hraId);

            return hraDto;

        }





    }
}
