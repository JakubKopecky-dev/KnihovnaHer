using System.Diagnostics;
using KnihovnaHer.Api.Settings;
using KnihovnaHer.Data.Models;
using KnihovnaHer.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KnihovnaHer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(UserManager<Uzivatel> userManager, IJwtTokenGenerator jwtTokenGenerator) : ControllerBase
    {

        private readonly UserManager<Uzivatel> userManager = userManager;
        private readonly IJwtTokenGenerator jwtTokenGenerator = jwtTokenGenerator;


        private async Task<UzivatelDto> ConvertToUserDto(Uzivatel user)
        {
            bool isAdmin = await userManager.IsInRoleAsync(user, UserRoles.Admin.ToString());

            return new UzivatelDto
            {
                UzivatelId = user.Id,
                Email = user.Email ?? throw new Exception("User email could not be found"),
                IsAdmin = isAdmin
            };

        }


        // registrace uživatele
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(AuthDto authDto)
        {
            var newUser = new Uzivatel
            {
                UserName = authDto.Email,
                Email = authDto.Email
            };

            var result = await userManager.CreateAsync(newUser, authDto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await userManager.AddToRoleAsync(newUser, UserRoles.User);

            var roles = await userManager.GetRolesAsync(newUser);
            var token = jwtTokenGenerator.GenerateToken(newUser, roles);

            var userDto = await ConvertToUserDto(newUser);

            return Ok(new
            {
                token,
                user = userDto
            });
        }

        //přihlášení uživatele

        [HttpPost]
        public async Task<IActionResult> LoginInUser(AuthDto authDto)
        {

            Uzivatel? user = await userManager.FindByEmailAsync(authDto.Email);

            if (user is null)
                return Unauthorized("Uživatel nebyl nalezen.");

           bool isPasswordValid = await userManager.CheckPasswordAsync(user, authDto.Password);

            if (!isPasswordValid)
                return Unauthorized("Neplatné přihlašovací údaje");

            var roles = await userManager.GetRolesAsync(user);
            var token = jwtTokenGenerator.GenerateToken(user, roles);   

            UzivatelDto userDto = await ConvertToUserDto(user);
            return Ok( new 
            { 
                token,
                user = userDto

            });
        }

    
        // získání přihlášeného uživatele
        [Authorize(Roles =UserRoles.User)]
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            Uzivatel? user = await userManager.GetUserAsync(User);

            if (user is not null)
            {
                UzivatelDto userDto = await ConvertToUserDto(user);
                Debug.WriteLine($"Uživatel {user.UserName} nalezen, vracím data.");

                return Ok(userDto);

            }
            Debug.WriteLine($"Uživatel {User.Identity?.Name} nebyl nalezen.");

            return Unauthorized();

        }




    }

}
