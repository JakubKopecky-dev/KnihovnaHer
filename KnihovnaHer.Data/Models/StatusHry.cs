using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KnihovnaHer.Data.Models
{
    public enum StavHry
    {
        Nova,Hraji,Dohrano
    }

    [Index(nameof(UzivatelId))]
    [Index(nameof(HraId))]
    public class StatusHry
    {
        public uint StatusHryId { get; set; }


        public  virtual Uzivatel Uzivatel { get; set; }
        public string UzivatelId { get; set; } = "";


        public virtual Hra Hra { get; set; }
        public uint HraId { get; set; }

        public StavHry Stav {  get; set; }

        public DateTime? DatumZacatku { get; set; }

        public DateTime? DatumDokonceni { get; set; }


        //hodncení hry
        [Range(0,10,ErrorMessage ="Zadej hodnocení mezi 0 až 10")]
        public int? Hodnoceni { get; set; }

        public string? Poznamka { get; set; }


    }
}
