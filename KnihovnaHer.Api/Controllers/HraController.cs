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
        public IEnumerable<HraDto> GetHry() => hraManager.GetAllHra();


        [Authorize(Roles = UserRoles.User)]
        [HttpGet("{hraId}")]
        public IActionResult GetHra(uint hraId)
        {
            HraDto? hraDto = hraManager.GetHra(hraId);

            if(hraDto is null) 
                return NotFound();

            return Ok(hraDto);

        }

        [Authorize(Roles =UserRoles.Admin)]
        [HttpPost]
        public IActionResult AddHra([FromBody] HraCreateEditDto hra)
        {
            HraDto addedHra = hraManager.AddHra(hra);

            return CreatedAtAction(nameof(GetHra), new { hraId = addedHra.HraId }, addedHra);


        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{hraId}")]
        public IActionResult EditHra(uint hraId, [FromBody] HraCreateEditDto hra)
        {
            HraDto? updatedHra = hraManager.UpdateHra(hraId, hra);

            if (updatedHra is null)
                return NotFound();

            return Ok(updatedHra);

        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{hraId}")]
        public IActionResult DeleteHra(uint hraId)
        {
           HraDto? deletedHra = hraManager.DeleteHra(hraId);

            if (deletedHra is null) 
                return NotFound();
           
            
            return Ok(deletedHra);
        }



















    }
}
