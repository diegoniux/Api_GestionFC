using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.DTO
{
    public class FoliosRecuperacionDTO
    {
        public Models.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public List<Models.FolioSolicitud> ListadoFolios { get; set; }

        public FoliosRecuperacionDTO()
        {
            this.ResultadoEjecucion = new Models.ResultadoEjecucion();
            this.ListadoFolios = new List<Models.FolioSolicitud>();
        }
    }
}
