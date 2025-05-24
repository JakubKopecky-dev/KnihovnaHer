using System.Text.Json.Serialization;
using KnihovnaHer.Dto;
using KnihovnaHer.Data.Models;

namespace KnihovnaHer.Dto
{
    public class HraDto
    {
        
        [JsonPropertyName("id")]
        public uint HraId { get; set; }
        public string Nazev { get; set; } = "";

        public int RokVydani { get; set; }


        public virtual List<string> Zanry { get; set; } = [];

        public virtual VydavatelDto? Vydavatel { get; set; }

        [JsonIgnore]
        public uint? VydavatelId { get; set; }

      

    }
}
