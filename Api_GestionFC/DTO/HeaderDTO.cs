using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models = Api_GestionFC.Models;

namespace Api_GestionFC.DTO
{
    public class HeaderDTO
    {
        public Models.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public string SaldoAcumulado { get; set; }
        public string SaldoVirtual { get; set; }
        public string SaldoSimulado { get; set; }
        public string PorcentajeCumplimiento{ get; set; }
        public string CumplimientoColor { get; set; }
        public string PorcentajeSimulacion { get; set; }
        public string SimulacionColor { get; set; }
        public int NumPlantilla { get; set; }
        public int NumAgentesMeta { get; set; }
        public int NumFCTInactivos { get; set; }
        public int NumTramitesCert { get; set; }
        public string Genero { get; set; }
        public string FotoGerente { get; set; }
        public string FotoGerenteColor { get; set; }

        public HeaderDTO() 
        {
            this.ResultadoEjecucion = new Models.ResultadoEjecucion();
        }
    }
}
