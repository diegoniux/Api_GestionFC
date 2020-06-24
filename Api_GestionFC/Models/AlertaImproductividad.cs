using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class AlertaImproductividad
    {
        public string Foto { get; set; }
        public int IdAlerta { get; set; }
        public int IdTipoAlerta { get; set; }
        public int IdEstatusAlerta { get; set; }
        public int NominaAP { get; set; }
        public string NombreAP { get; set; }
        public string ApellidoAP { get; set; }

        public AlertaImproductividad()
        {

        }
    }
}
