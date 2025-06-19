using AutoMapper;
using KnihovnaHer.Api.Interfaces;
using KnihovnaHer.Dto;
using KnihovnaHer.Data.Interfaces;
using KnihovnaHer.Data.Models;
using System.Threading.Tasks;

namespace KnihovnaHer.Api.Managers
{
    public class VydavatelManager (IVydavatelRepository vydavatelRepository, IMapper mapper) : IVydavatelManager
    {

        private readonly IVydavatelRepository vydavatelRepository = vydavatelRepository;
        private readonly IMapper mapper = mapper;




        public async Task<IList<VydavatelDto>> GetAllVydavatelAsync()
        {
            IList<Vydavatel> vydavatel = await vydavatelRepository.GetAllAsync();

            return mapper.Map<IList<VydavatelDto>>(vydavatel);

        }

        public async Task<VydavatelDto?> GetVydavatelAsync(uint id)
        {
            Vydavatel? vydavatel = await vydavatelRepository.FindByIdAsync(id);
            if (vydavatel is null)
                return null;

            return mapper.Map<VydavatelDto>(vydavatel);


        }


        public async Task<VydavatelDto> AddVydavatelAsync(VydavatelDto vydavatelDto)
        {
            Vydavatel vydavatel = mapper.Map<Vydavatel>(vydavatelDto);
            vydavatel.VydavatelId = default;
            Vydavatel addedVydavatel = await vydavatelRepository.InsertAsync(vydavatel);

            return mapper.Map<VydavatelDto>(addedVydavatel);


        }


        public async Task<VydavatelDto?> EditVydavatelAsync(uint vydatavatelId,VydavatelDto vydavatelDto)
        {
         
            if(! await vydavatelRepository.ExistsWithIdAsync(vydatavatelId))
                return null;

            Vydavatel vydavatel = mapper.Map<Vydavatel>(vydavatelDto);
            vydavatel.VydavatelId = vydatavatelId;
            Vydavatel uppdatedVydavatel = await vydavatelRepository.UpdateAsync(vydavatel);

            return mapper.Map<VydavatelDto>(uppdatedVydavatel);


        }


        public async Task<VydavatelDto?> DeleteVydavatelAsync(uint vydavatelId)
        {
            Vydavatel? vydavatel = await vydavatelRepository.FindByIdAsync(vydavatelId);
            if(vydavatel is null)
                return null;

            VydavatelDto deletedVydavatel = mapper.Map<VydavatelDto>(vydavatel);

            vydavatel.Hry.Clear();
           await vydavatelRepository.UpdateAsync(vydavatel);
           await vydavatelRepository.DeleteAsync(vydavatelId);

            return deletedVydavatel;

            

        }








    }
}
