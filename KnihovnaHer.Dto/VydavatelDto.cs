using KnihovnaHer.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KnihovnaHer.Dto
{
    public class VydavatelDto
    {
        [JsonPropertyName("id")]    
        public uint VydavatelId { get; set; }

        [MinLength(2, ErrorMessage = "Název musí mít alespoň 2 znaky.")]
        public string Nazev { get; set; } = "";

        


    }
}
