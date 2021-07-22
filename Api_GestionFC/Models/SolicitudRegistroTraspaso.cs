using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class SolicitudRegistroTraspaso
    {
        public int RegistroTraspasoId { get; set; }
        public string FolioSolicitud { get; set; }
        public int EstatusId { get; set; }
        public string EstatusDescripcion { get; set; }
        public int Seccion { get; set; }
    }
}
