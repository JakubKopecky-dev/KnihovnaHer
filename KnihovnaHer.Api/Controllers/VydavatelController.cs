using System.Threading.Tasks;
using KnihovnaHer.Api.Interfaces;
using KnihovnaHer.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnihovnaHer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VydavatelController (IVydavatelManager vydavatelManager) : ControllerBase
    {

        private readonly IVydavatelManager vydavatelManager = vydavatelManager;


        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        public async Task<IEnumerable<VydavatelDto>> GetVydavatel() => await vydavatelManager.GetAllVydavatelAsync();


        [Authorize(Roles = UserRoles.User)]
        [HttpGet("{vydavatelId}")]
        public async Task<IActionResult> GetVydavatel(uint vydavatelId)
        {
            VydavatelDto? vydavatelDto = await vydavatelManager.GetVydavatelAsync(vydavatelId);

            if(vydavatelDto is null) 
                return NotFound();

            return Ok(vydavatelDto);

        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> AddVydavatel([FromBody] VydavatelDto vydavatelDto)
        {
            VydavatelDto addedvydavatel = await vydavatelManager.AddVydavatelAsync(vydavatelDto);

            return CreatedAtAction(nameof(GetVydavatel), new {vydavatelId = addedvydavatel.VydavatelId}, addedvydavatel);


        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{vydavatelId}")]
        public async Task<IActionResult> UpdateVydavatel(uint vydavatelId,[FromBody] VydavatelDto vydavatelDto)
        {
            VydavatelDto? updatedVydatel = await vydavatelManager.EditVydavatelAsync(vydavatelId, vydavatelDto);

            if(updatedVydatel is null)
                return NotFound();

            return Ok(updatedVydatel);

        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{vydavatelId}")]
        public async Task<IActionResult> DeleteVydavatel(uint vydavatelId)
        {
            VydavatelDto? deletedVydavatel = await vydavatelManager.DeleteVydavatelAsync(vydavatelId);
            if(deletedVydavatel is null)
                return NotFound();
            return Ok(deletedVydavatel);

        }



    }
}
