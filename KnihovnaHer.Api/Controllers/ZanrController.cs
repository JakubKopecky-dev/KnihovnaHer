using KnihovnaHer.Api.Interfaces;
using KnihovnaHer.Dto;
using KnihovnaHer.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace KnihovnaHer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZanrController(IZanrManager zanrManager) : ControllerBase
    {
        private readonly IZanrManager zanrManager = zanrManager;



        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        public async Task<IEnumerable<ZanrDto>> GetZanr() => await zanrManager.GetAllZanrAsync();



        [Authorize(Roles = UserRoles.User)]
        [HttpGet("{zanrId}")]
        public async Task<IActionResult> GetZanr(uint zanrId)
        {
            ZanrDto? zanr = await zanrManager.GetZanrAsync(zanrId);

            if(zanr is null)
                return NotFound();

            return Ok(zanr);
        }




        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async  Task<IActionResult> AddZanr([FromBody]  ZanrDto zanrDto)
        {
            ZanrDto addedZanr = await zanrManager.AddZanrAsync(zanrDto);

            return CreatedAtAction(nameof(GetZanr), new {zanrId = addedZanr.ZanrId}, addedZanr);
        }



        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{zanrId}")]
        public async Task<IActionResult> DeleteZanr(uint zanrId)
        {
            ZanrDto? deletedZanr = await zanrManager.DeleteZanrAsync(zanrId);

            if(deletedZanr is null)
                return NotFound();

            return Ok(deletedZanr);

        }







    }
}
