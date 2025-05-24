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
        public IEnumerable<ZanrDto> GetZanr() => zanrManager.GetAllZanr();


        [Authorize(Roles = UserRoles.User)]
        [HttpGet("{zanrId}")]
        public IActionResult GetZanr(uint zanrId)
        {
            ZanrDto? zanr = zanrManager.GetZanr(zanrId);

            if(zanr is null)
                return NotFound();

            return Ok(zanr);
        }




        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public IActionResult AddZanr([FromBody]  ZanrDto zanrDto)
        {
            ZanrDto addedZanr = zanrManager.AddZanr(zanrDto);

            return CreatedAtAction(nameof(GetZanr), new {zanrId = addedZanr.ZanrId}, addedZanr);
        }



        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{zanrId}")]
        public IActionResult DeleteZanr(uint zanrId)
        {
            ZanrDto? deletedZanr = zanrManager.DeleteZanr(zanrId);

            if(deletedZanr is null)
                return NotFound();

            return Ok(deletedZanr);

        }







    }
}
