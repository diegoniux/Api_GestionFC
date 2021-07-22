using Api_GestionFC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.DTO
{
    public class AlertaInvestigacionDTO
    {
        public Models.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public int Cantidad { get; set; }
        public List<AlertaInvestigacion> ResultDatos { get; set; }

        public AlertaInvestigacionDTO()
        {
            this.ResultadoEjecucion = new ResultadoEjecucion();
            this.ResultDatos = new List<AlertaInvestigacion>();
        }

    }
}
