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


        public IList<ZanrDto> GetAllZanr()
        {
            IList<Zanr> zanry = zanrRepository.GetAll();
            
            return mapper.Map<IList<ZanrDto>>(zanry);
        }
        

        public ZanrDto? GetZanr(uint zanrId)
        {
            Zanr? zanr = zanrRepository.FindById(zanrId);

            if(zanr is null) 
                return null;

            return mapper.Map<ZanrDto>(zanr);


        }



        public ZanrDto AddZanr(ZanrDto zanrDto)
        {
            Zanr zanr = mapper.Map<Zanr>(zanrDto);
            zanr.ZanrId = default;
            Zanr addedZanr = zanrRepository.Insert(zanr);

            return mapper.Map<ZanrDto>(addedZanr);

        }

      

        public ZanrDto? DeleteZanr(uint zanrId)
        {
            if (!zanrRepository.ExistsWithId(zanrId))
                return null;

            Zanr zanrDb = zanrRepository.FindById(zanrId)!;

            ZanrDto deletedZanr = mapper.Map<ZanrDto>(zanrDb);

            zanrDb.Hry.Clear();
            zanrRepository.Update(zanrDb);
           
            zanrRepository.Delete(zanrId);

            return deletedZanr;


        }








    }
}
