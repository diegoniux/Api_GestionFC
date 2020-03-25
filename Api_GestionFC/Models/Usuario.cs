using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class Usuario
    {
        [Key]
        public int Nomina { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
    }
}
