using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KnihovnaHer.Data.Models
{
    [Index(nameof(Nazev))]
    public class Zanr
    {
        public uint ZanrId { get; set; }

        [MinLength(2)]
        public string Nazev { get; set; } = "";

        public virtual List<Hra> Hry { get; set; } = [];

       

    }
}
