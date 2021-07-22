using Api_GestionFC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.DTO
{
    public class AlertaImproductividadDTO
    {
        public Models.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public int Cantidad { get; set; }
        public List<AlertaImproductividad> ResultDatos { get; set; }

        public AlertaImproductividadDTO()
        {
            this.ResultadoEjecucion = new ResultadoEjecucion();
            this.ResultDatos = new List<AlertaImproductividad>();
        }

    }
}
