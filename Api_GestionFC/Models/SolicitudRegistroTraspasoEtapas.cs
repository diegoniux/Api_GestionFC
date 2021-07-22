using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class SolicitudRegistroTraspasoEtapas
    {
        public int RegistroTraspasoId { get; set; }
        public int EtapaId { get; set; }
        public string EtapaDescripcion { get; set; }
        public string Usuario { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
    }
}
