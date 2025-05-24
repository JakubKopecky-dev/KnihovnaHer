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
        public IEnumerable<VydavatelDto> GetVydavatel() => vydavatelManager.GetAllVydavatel();


        [Authorize(Roles = UserRoles.User)]
        [HttpGet("{vydavatelId}")]
        public IActionResult GetVydavatel(uint vydavatelId)
        {
            VydavatelDto? vydavatelDto = vydavatelManager.GetVydavatel(vydavatelId);

            if(vydavatelDto is null) 
                return NotFound();

            return Ok(vydavatelDto);

        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public IActionResult AddVydavatel([FromBody] VydavatelDto vydavatelDto)
        {
            VydavatelDto addedvydavatel = vydavatelManager.AddVydavatel(vydavatelDto);

            return CreatedAtAction(nameof(GetVydavatel), new {vydavatelId = addedvydavatel.VydavatelId}, addedvydavatel);


        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{vydavatelId}")]
        public IActionResult UpdateVydavatel(uint vydavatelId,[FromBody] VydavatelDto vydavatelDto)
        {
            VydavatelDto? updatedVydatel = vydavatelManager.EditVydavatel(vydavatelId, vydavatelDto);

            if(updatedVydatel is null)
                return NotFound();

            return Ok(updatedVydatel);

        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{vydavatelId}")]
        public IActionResult DeleteVydavatel(uint vydavatelId)
        {
            VydavatelDto? deletedVydavatel = vydavatelManager.DeleteVydavatel(vydavatelId);
            if(deletedVydavatel is null)
                return NotFound();
            return Ok(deletedVydavatel);

        }



    }
}
