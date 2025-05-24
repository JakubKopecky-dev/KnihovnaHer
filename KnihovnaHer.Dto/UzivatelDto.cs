using KnihovnaHer.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KnihovnaHer.Dto
{
    public class UzivatelDto
    {
        [JsonPropertyName("id")]
        public string UzivatelId { get; set; } = "";

        [EmailAddress]
        public string Email { get; set; } = "";

        public bool IsAdmin { get; set; }





    }
}
