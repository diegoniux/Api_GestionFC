using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class DetalleHistoricoHeader
    {
        public bool HabilitarAdelantar { get; set; }
        public string FechaIniFin { get; set; }
        public bool HabilitarAnterior { get; set; }
    }
}
