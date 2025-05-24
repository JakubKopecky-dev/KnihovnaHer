using AutoMapper;
using KnihovnaHer.Api.Interfaces;
using KnihovnaHer.Dto;
using KnihovnaHer.Data.Interfaces;
using KnihovnaHer.Data.Models;

namespace KnihovnaHer.Api.Managers
{
    public class VydavatelManager (IVydavatelRepository vydavatelRepository, IMapper mapper) : IVydavatelManager
    {

        private readonly IVydavatelRepository vydavatelRepository = vydavatelRepository;
        private readonly IMapper mapper = mapper;




        public IList<VydavatelDto> GetAllVydavatel()
        {
            IList<Vydavatel> vydavatel = vydavatelRepository.GetAll();

            return mapper.Map<IList<VydavatelDto>>(vydavatel);

        }

        public VydavatelDto? GetVydavatel(uint id)
        {
            Vydavatel? vydavatel = vydavatelRepository.FindById(id);
            if (vydavatel is null)
                return null;

            return mapper.Map<VydavatelDto>(vydavatel);


        }


        public VydavatelDto AddVydavatel(VydavatelDto vydavatelDto)
        {
            Vydavatel vydavatel = mapper.Map<Vydavatel>(vydavatelDto);
            vydavatel.VydavatelId = default;
            Vydavatel addedVydavatel = vydavatelRepository.Insert(vydavatel);

            return mapper.Map<VydavatelDto>(addedVydavatel);


        }


        public VydavatelDto? EditVydavatel(uint vydatavatelId,VydavatelDto vydavatelDto)
        {
         
            if(!vydavatelRepository.ExistsWithId(vydatavatelId))
                return null;

            Vydavatel vydavatel = mapper.Map<Vydavatel>(vydavatelDto);
            vydavatel.VydavatelId = vydatavatelId;
            Vydavatel uppdatedVydavatel = vydavatelRepository.Update(vydavatel);

            return mapper.Map<VydavatelDto>(uppdatedVydavatel);


        }


        public VydavatelDto? DeleteVydavatel(uint vydavatelId)
        {
            Vydavatel? vydavatel = vydavatelRepository.FindById(vydavatelId);
            if(vydavatel is null)
                return null;

            VydavatelDto deletedVydavatel = mapper.Map<VydavatelDto>(vydavatel);

            vydavatel.Hry.Clear();
            vydavatelRepository.Update(vydavatel);
            vydavatelRepository.Delete(vydavatelId);

            return deletedVydavatel;

            

        }








    }
}
