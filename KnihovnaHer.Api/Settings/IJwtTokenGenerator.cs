using KnihovnaHer.Data.Models;

namespace KnihovnaHer.Api.Settings
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Uzivatel uzivatel, IList<string> roles);
    }
}
