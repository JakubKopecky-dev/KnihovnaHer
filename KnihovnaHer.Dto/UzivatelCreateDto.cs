using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnihovnaHer.Dto
{
    public class UzivatelCreateDto 
    {
        [EmailAddress]
        public string Email { get; set; } = "";

        public bool IsAdmin { get; set; }
        public string Password { get; set; } = "";

    }
}
