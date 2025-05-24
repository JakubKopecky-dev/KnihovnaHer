using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KnihovnaHer.Dto
{
    public class HraCreateEditDto
    {
       

        [MinLength(2, ErrorMessage = "Název musí mít alespoň 2 znaky.")]
        public string Nazev { get; set; } = "";

        public int RokVydani { get; set; }


        public List<string> Zanry { get; set; } = [];

       
       
        public uint? VydavatelId { get; set; }

    }
}
