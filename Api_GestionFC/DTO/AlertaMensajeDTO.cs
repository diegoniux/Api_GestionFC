using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.DTO
{
    public class AlertaMensajeDTO
    {
        public Models.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public string Mensaje { get; set; }
        public string SaldoAcomuladoMeta { get; set; }

        public AlertaMensajeDTO()
        {
            this.ResultadoEjecucion = new Models.ResultadoEjecucion();
        }
    }
}
