using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using KnihovnaHer.Data.Models;
using KnihovnaHer.Dto;

namespace KnihovnaHer.Dto
{
    public class StatusHryViewDto
    {
        public uint StatusHryId { get; set; }

        
        public UzivatelDto? Uzivatel { get; set; }

        [JsonIgnore]
        public string UzivatelId { get; set; } = "";


        public HraDto? Hra { get; set; }

    
        public StavHry Stav { get; set; }

        public DateTime? DatumZacatku { get; set; }

        public DateTime? DatumDokonceni { get; set; }


        //hodncení hry

        public int? Hodnoceni { get; set; }

        public string? Poznamka { get; set; }
    }
}
