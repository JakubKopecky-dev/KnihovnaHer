using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KnihovnaHer.Data.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace KnihovnaHer.Api.Settings
{
    public class JwtTokenGenerator(IOptions<JwtSettings> jwtSettings) : IJwtTokenGenerator
    {
        private readonly JwtSettings jwtSettings = jwtSettings.Value;

        public string GenerateToken(Uzivatel uzivatel, IList<string> role)
        {
            // Přidání základních informací do tokenu
            var claims = new List<Claim>();



            // Přidání identifikátoru uživatele (nutné pro UserManager.GetUserAsync)
            if (!string.IsNullOrEmpty(uzivatel.Id))
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, uzivatel.Id));
            }



            // Kontrola, zda není UserName null nebo prázdný, jinak použít výchozí hodnotu
            if (!string.IsNullOrEmpty(uzivatel.UserName))
            {
                claims.Add(new Claim(ClaimTypes.Name, uzivatel.UserName));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Name, "UnknownUser"));
            }




            // Kontrola, zda není Email null nebo prázdný, jinak použít výchozí hodnotu
            if (!string.IsNullOrEmpty(uzivatel.Email))
            {
                claims.Add(new Claim(ClaimTypes.Email, uzivatel.Email));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Email, "UnknownEmail"));
            }




            // Přidání rolí do claimu
            if (role != null && role.Any())
            {
                foreach (var r in role)
                {
                    claims.Add(new Claim(ClaimTypes.Role, r));
                }
            }




            // Vytvoření podpisu tokenu
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                SecurityAlgorithms.HmacSha256
            );



            // Nastavení expirace tokenu
            var expiry = DateTime.Now.AddMinutes(jwtSettings.ExpiresInMinutes);

            // Vytvoření tokenu
            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: expiry,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
