using KnihovnaHer.Data.Models;
using System.Text.Json.Serialization;

namespace KnihovnaHer.Dto
{
    public class StatusHryUpdateDto
    {
       

        public StavHry Stav { get; set; }


        //hodncení hry

        public int? Hodnoceni { get; set; }

        public string? Poznamka { get; set; }
    }
}
