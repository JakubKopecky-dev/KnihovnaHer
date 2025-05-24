using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace KnihovnaHer.Data.Models
{
    public class Uzivatel : IdentityUser
    {

        public bool IsAdmin { get; set; }
        public virtual List<StatusHry> StatusHer { get; set; } =[];

    }
}
