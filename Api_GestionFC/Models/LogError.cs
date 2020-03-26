using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class LogError
    {
        public int IdPantalla { get; set; }
        public int Usuario { get; set; }
        public string Error { get; set; }
        public string Dispositivo { get; set; }
    }
}
