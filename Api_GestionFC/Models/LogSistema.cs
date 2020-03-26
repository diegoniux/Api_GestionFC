using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class LogSistema
    {
        [Key]
        public int IdAccion { get; set; }
        public int IdPantalla { get; set; }
        public int Usuario { get; set; }
        public string Dispositivo { get; set; }
    }
}
