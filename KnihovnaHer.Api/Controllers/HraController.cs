using KnihovnaHer.Api.Interfaces;
using KnihovnaHer.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnihovnaHer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HraController(IHraManager hraManager) : ControllerBase
    {

        private readonly IHraManager hraManager = hraManager;


        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        public async Task<IEnumerable<HraDto>> GetHry() => await hraManager.GetAllHraAsync();


        [Authorize(Roles = UserRoles.User)]
        [HttpGet("{hraId}")]
        public async Task<IActionResult> GetHra(uint hraId)
        {
            HraDto? hraDto = await hraManager.GetHraAsync(hraId);

            if(hraDto is null) 
                return NotFound();

            return Ok(hraDto);

        }

        [Authorize(Roles =UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> AddHra([FromBody] HraCreateEditDto hra)
        {
            HraDto addedHra = await hraManager.AddHraAsync(hra);

            return CreatedAtAction(nameof(GetHra), new { hraId = addedHra.HraId }, addedHra);


        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{hraId}")]
        public async Task<IActionResult> EditHra(uint hraId, [FromBody] HraCreateEditDto hra)
        {
            HraDto? updatedHra = await hraManager.UpdateHra(hraId, hra);

            if (updatedHra is null)
                return NotFound();

            return Ok(updatedHra);

        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{hraId}")]
        public async Task<IActionResult> DeleteHra(uint hraId)
        {
           HraDto? deletedHra = await hraManager.DeleteHra(hraId);

            if (deletedHra is null) 
                return NotFound();
           
            
            return Ok(deletedHra);
        }



















    }
}
