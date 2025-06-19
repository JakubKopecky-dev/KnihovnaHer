using AutoMapper;
using KnihovnaHer.Api.Interfaces;
using KnihovnaHer.Dto;
using KnihovnaHer.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KnihovnaHer.Data.Interfaces;

namespace KnihovnaHer.Api.Managers
{
    public class UzivatelManager(UserManager<Uzivatel> userManager, IMapper mapper,IStatusHryRepository statusHryRepository) : IUzivatelManager
    {

        private readonly UserManager<Uzivatel> userManager = userManager;
        private readonly IMapper mapper = mapper;
        private readonly IStatusHryRepository statusHryRepository = statusHryRepository;


        // získání všech uživatelů
        public async Task<IList<UzivatelDto>> GetAllUzivatelAsync()
        {
            var uzivatele =  await userManager.Users.ToListAsync();


            return mapper.Map<IList<UzivatelDto>>(uzivatele);
           
        }


        // získat jednoho uživatele
        public async Task<UzivatelDto?> GetUzivatelByIdAsync(string id)
        {
            Uzivatel? uzivatel = await userManager.FindByIdAsync(id);

            if(uzivatel is null)
                return null;

            return mapper.Map<UzivatelDto>(uzivatel);
        }

        
        // vytvoření uživatele
        public async Task<UzivatelDto?> AddUzivatelAsnyc(UzivatelCreateDto uzivatelDto)
        {
            Uzivatel? uzivatel = mapper.Map<Uzivatel>(uzivatelDto);
            var result = await userManager.CreateAsync(uzivatel, uzivatelDto.Password);


            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(uzivatel, UserRoles.User);

                if(uzivatel.IsAdmin) 
                    await userManager.AddToRoleAsync(uzivatel, UserRoles.Admin);


                return mapper.Map<UzivatelDto>(uzivatel);
            }
            
            return null;

        }

        // aktualizace uživatele
        public async Task<UzivatelDto?> UpdateUzivatelAsync(string id, UzivatelEditDto uzivatelDto)
        {
            Uzivatel? uzivatel = await userManager.FindByIdAsync(id);

            if (uzivatel is null)
                return null;

            bool wasAdmin = await userManager.IsInRoleAsync(uzivatel,UserRoles.Admin);

            mapper.Map<UzivatelEditDto,Uzivatel>(uzivatelDto, uzivatel);

            var result = await userManager.UpdateAsync(uzivatel);

            if (result.Succeeded)
            {
                if (!uzivatelDto.IsAdmin && wasAdmin)
                    await userManager.RemoveFromRoleAsync(uzivatel, UserRoles.Admin);

                else if(uzivatel.IsAdmin && !wasAdmin)
                    await userManager.AddToRoleAsync(uzivatel , UserRoles.Admin);


                return mapper.Map<UzivatelDto>(uzivatel);
            }

            return null;

        }

        //odstranění uživatele

        public async Task<UzivatelDto?> DeleteUzivatelAsync(string id)
        {
            Uzivatel? uzivatel = await userManager.FindByIdAsync(id);

            if (uzivatel is null)
                return null;

            UzivatelDto uzivatelDto = mapper.Map<UzivatelDto>(uzivatel);


            var roles = await userManager.GetRolesAsync(uzivatel);
            if (roles.Any())
                await userManager.RemoveFromRolesAsync(uzivatel, roles);

            var statusyHerUzivatele = await statusHryRepository.FindByUzivatelIdAsync(id);

            foreach(var s in statusyHerUzivatele)
               await statusHryRepository.DeleteAsync(s.StatusHryId);

            await userManager.UpdateAsync(uzivatel);

            


            var result = await userManager.DeleteAsync(uzivatel);

            if (result.Succeeded)
                return uzivatelDto;

            return null ;
        }






    }
}
