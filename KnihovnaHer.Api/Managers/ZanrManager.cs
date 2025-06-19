using AutoMapper;
using KnihovnaHer.Api.Interfaces;
using KnihovnaHer.Dto;
using KnihovnaHer.Data.Interfaces;
using KnihovnaHer.Data.Models;

namespace KnihovnaHer.Api.Managers
{
    public class ZanrManager (IMapper mapper, IZanrRepository zanrRepository) : IZanrManager
    {
        private readonly IMapper mapper = mapper;
        private readonly IZanrRepository zanrRepository = zanrRepository;


        public async Task<IList<ZanrDto>> GetAllZanrAsync()
        {
            IList<Zanr> zanry = await zanrRepository.GetAllAsync();
            
            return mapper.Map<IList<ZanrDto>>(zanry);
        }
        

        public async Task<ZanrDto?> GetZanrAsync(uint zanrId)
        {
            Zanr? zanr = await zanrRepository.FindByIdAsync(zanrId);

            if(zanr is null) 
                return null;

            return mapper.Map<ZanrDto>(zanr);


        }



        public async Task<ZanrDto> AddZanrAsync(ZanrDto zanrDto)
        {
            Zanr zanr = mapper.Map<Zanr>(zanrDto);
            zanr.ZanrId = default;
            Zanr addedZanr = await zanrRepository.InsertAsync(zanr);

            return mapper.Map<ZanrDto>(addedZanr);

        }

      

        public async Task<ZanrDto?> DeleteZanrAsync(uint zanrId)
        {
            if (! await zanrRepository.ExistsWithIdAsync(zanrId))
                return null;

            Zanr zanrDb = (await zanrRepository.FindByIdAsync(zanrId))!;

            ZanrDto deletedZanr = mapper.Map<ZanrDto>(zanrDb);

            zanrDb.Hry.Clear();
            await zanrRepository.UpdateAsync(zanrDb);
           
            await zanrRepository.DeleteAsync(zanrId);

            return deletedZanr;


        }








    }
}
