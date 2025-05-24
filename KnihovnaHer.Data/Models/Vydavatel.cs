using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaHer.Data.Models
{
    public class Vydavatel
    {
        public uint VydavatelId { get; set; }

        [MinLength(3)]
        public string Nazev { get; set; } = "";

        public virtual List<Hra> Hry { get; set; } = new();

    }
}
