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
    public class StatusHryController : ControllerBase
    {
        private readonly IStatusHryManager statusHryManager;
        private readonly UserManager<Uzivatel> userManager;

        public StatusHryController(IStatusHryManager statusHryManager, UserManager<Uzivatel> userManager)
        {
            this.statusHryManager = statusHryManager;
            this.userManager = userManager;
            Debug.WriteLine("✅ StatusHryController byl vytvořen");

        } 


        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusHryViewDto>>> GetStatusHry()
        {
           
            var user = await userManager.GetUserAsync(User);

            if(user is null)
                return Unauthorized();
                
            var statusHry = await statusHryManager.GetAllStatusForUser(user.Id);

            return Ok(statusHry);
            
        }


        
        [HttpPost]
        public async Task<IActionResult> AddStatusHry([FromBody]StatusHryCreateDto statusHryCreateDto)
        {

            var user = await userManager.GetUserAsync(User);

            if(user is null)
                return Unauthorized();

            

            StatusHryViewDto status = statusHryManager.AddStatusHry(statusHryCreateDto,user.Id);

            return CreatedAtAction(nameof(GetStatusHry), new {uzivatelId = user.Id}, status);


        }


        [HttpPut("{statusHryId}")]
        public IActionResult EditStatusHry(uint statusHryId,[FromBody]StatusHryEditDto statusHryEditDto)
        {
            StatusHryViewDto? updatedStatusHry = statusHryManager.EditStatusHry(statusHryId, statusHryEditDto);
            if (updatedStatusHry is null)
                return NotFound();

            return Ok(updatedStatusHry);


        }


        [HttpDelete("{statusHryId}")]
        public IActionResult DeleteStatusHry(uint statusHryId)
        {
            StatusHryViewDto? statusHryViewDto = statusHryManager.DeleteStatusHry(statusHryId);
            if(statusHryViewDto is null)
                return NotFound();
            return Ok(statusHryViewDto);

        }








    }
}
