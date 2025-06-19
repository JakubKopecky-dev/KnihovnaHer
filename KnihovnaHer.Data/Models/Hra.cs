using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaHer.Data.Models
{
    public class Hra
    {
        public uint HraId { get; set; }

        [MinLength(2)]
        public string Nazev { get; set; } = "";

        public int RokVydani { get; set; }

      
      public virtual List<Zanr> Zanry { get; set; } = [];

        public virtual Vydavatel? Vydavatel { get; set; }

        public uint? VydavatelId { get; set; }


        public virtual List<StatusHry> StatusHer { get; set; } = [];

       




    }
}
