using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KnihovnaHer.Dto
{
    public class AuthResponseDto
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = "";

        [JsonPropertyName("user")]
        public required UzivatelDto User { get; set; }
    }
}
