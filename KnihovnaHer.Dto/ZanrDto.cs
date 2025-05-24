using KnihovnaHer.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace KnihovnaHer.Dto
{
    public class ZanrDto
    {
        
        public uint ZanrId { get; set; }

        [MinLength(2, ErrorMessage = "Název musí mít alespoň 2 znaky.")]
        public string Nazev { get; set; } = "";

    }
}
