using Api_GestionFC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.DTO
{
    public class AlertaRecuperacionDTO
    {
        public Models.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public int Cantidad { get; set; }
        public List<AlertaRecuperacion> ResultDatos { get; set; }

        public AlertaRecuperacionDTO()
        {
            this.ResultadoEjecucion = new ResultadoEjecucion();
            this.ResultDatos = new List<AlertaRecuperacion>();
        }

    }
}
