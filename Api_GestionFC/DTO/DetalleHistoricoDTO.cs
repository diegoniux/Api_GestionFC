using Api_GestionFC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.DTO
{
    public class DetalleHistoricoDTO
    {
        public Models.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public DetalleHistoricoHeader detalleHistoricoHeader { get; set; }
        public DetalleHistoricoSemanas detalleHistoricoSemanas { get; set; }
        public List<DetalleHistoricoTramitesRecup> detalleHistoricoTramites { get; set; }
        public List<DetalleHistoricoTramitesRecup> detalleHistoricoRecuperacion { get; set; }

        public DetalleHistoricoDTO()
        {
            this.ResultadoEjecucion = new ResultadoEjecucion();
            this.detalleHistoricoHeader = new DetalleHistoricoHeader();
            this.detalleHistoricoSemanas = new DetalleHistoricoSemanas();
            this.detalleHistoricoTramites = new List<DetalleHistoricoTramitesRecup>();
            this.detalleHistoricoRecuperacion = new List<DetalleHistoricoTramitesRecup>();
        }
    }
}
