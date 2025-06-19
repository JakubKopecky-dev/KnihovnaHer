using System.Diagnostics;
using System.Threading.Tasks;
using KnihovnaHer.Api.Interfaces;
using KnihovnaHer.Data.Models;
using KnihovnaHer.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KnihovnaHer.Api.Controllers
{
    [Authorize(Roles =UserRoles.User)]
    [ApiController]
    [Route("api/[controller]")]
    public class StatusHryController(IStatusHryManager statusHryManager, UserManager<Uzivatel> userManager) : ControllerBase
    {
        private readonly IStatusHryManager statusHryManager = statusHryManager;
        private readonly UserManager<Uzivatel> userManager = userManager;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusHryViewDto>>> GetStatusHry()
        {
           
            var user = await userManager.GetUserAsync(User);

            if(user is null)
                return Unauthorized();
                
            var statusHry = await statusHryManager.GetAllStatusForUserAsync(user.Id);

            return Ok(statusHry);
            
        }


        
        [HttpPost]
        public async Task<IActionResult> AddStatusHry([FromBody]StatusHryCreateDto statusHryCreateDto)
        {

            var user = await userManager.GetUserAsync(User);

            if(user is null)
                return Unauthorized();

            

            StatusHryViewDto status = await statusHryManager.AddStatusHryAsync(statusHryCreateDto,user.Id);

            return CreatedAtAction(nameof(GetStatusHry), new {uzivatelId = user.Id}, status);


        }


        [HttpPut("{statusHryId}")]
        public async Task<IActionResult> EditStatusHry(uint statusHryId,[FromBody]StatusHryUpdateDto statusHryEditDto)
        {
            StatusHryViewDto? updatedStatusHry = await statusHryManager.UpdateStatusHryAsync(statusHryId, statusHryEditDto);
            if (updatedStatusHry is null)
                return NotFound();

            return Ok(updatedStatusHry);


        }


        [HttpDelete("{statusHryId}")]
        public async Task<IActionResult> DeleteStatusHry(uint statusHryId)
        {
            StatusHryViewDto? statusHryViewDto = await statusHryManager.DeleteStatusHry(statusHryId);
            if(statusHryViewDto is null)
                return NotFound();
            return Ok(statusHryViewDto);

        }








    }
}
