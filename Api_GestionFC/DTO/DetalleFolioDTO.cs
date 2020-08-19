using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_GestionFC.Models;

namespace Api_GestionFC.DTO
{
    public class DetalleFolioDTO
    {
        public Models.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public SolicitudRegistroTraspaso detalleFolios { get; set; }
        public SolicitudRegistroTraspasoEtapas detalleEtapas { get; set; }

        public DetalleFolioDTO()
        {
            this.ResultadoEjecucion = new ResultadoEjecucion();
            this.detalleFolios = new SolicitudRegistroTraspaso();
            this.detalleEtapas = new SolicitudRegistroTraspasoEtapas();
        }
    }
}
