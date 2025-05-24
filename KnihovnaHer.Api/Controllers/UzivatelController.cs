using KnihovnaHer.Api.Interfaces;
using KnihovnaHer.Dto;
using KnihovnaHer.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace KnihovnaHer.Api.Controllers
{

   // [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("api/[controller]")]

    public class UzivatelController(IUzivatelManager uzivatelManager) : ControllerBase
    {
        private readonly IUzivatelManager uzivatelManager = uzivatelManager;


        // vypsání všech uživatelů
        [HttpGet]
        public async Task<IEnumerable<UzivatelDto>> GetUzivatele() => await uzivatelManager.GetAllUzivatelAsync();



        //vypsání uživatele
        [HttpGet("{uzivatelId}")]
        public async Task<IActionResult> GetUzivatel(string uzivatelId)
        {
            UzivatelDto? uzivatel = await uzivatelManager.GetUzivatelByIdAsync(uzivatelId);

            if(uzivatel is null)
                return NotFound();

            return Ok(uzivatel);


        }

        // přidání uživatele
        [HttpPost]
        public async Task<IActionResult> AddUzivate([FromBody]UzivatelCreateDto uzivatel)
        {
            UzivatelDto? createdUzivatel = await uzivatelManager.AddUzivatelAsnyc(uzivatel);

            if(createdUzivatel is null)
                return NotFound();

            return CreatedAtAction(nameof(GetUzivatel),new {uzivatelId = createdUzivatel.UzivatelId},createdUzivatel);

        }



        // Update uživatele
        [HttpPut("{uzivatelId}")]
        public async Task<IActionResult> EditUzivatel(string uzivatelId, UzivatelEditDto uzivatel)
        {
            UzivatelDto? updatedUzivatel = await uzivatelManager.UpdateUzivatelAsync(uzivatelId, uzivatel);

            if(updatedUzivatel is null)
                return NotFound();

            return Ok(updatedUzivatel);

        }


        //odstranění uživatele
        [HttpDelete("{uzivatelId}")]
        public async Task<IActionResult> DeleteUzivatel(string uzivatelId)
        {
            UzivatelDto? deletedUzivatel = await uzivatelManager.DeleteUzivatelAsync(uzivatelId);

            if(deletedUzivatel is null)
                return NotFound();

            return Ok(deletedUzivatel);

        }




    }
}
