using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class LoginData
    {
        [Key]
        public int Nomina { get; set; }
        public string Password { get; set; }
    }
}
