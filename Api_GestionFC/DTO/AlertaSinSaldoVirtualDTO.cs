using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_GestionFC.Models;

namespace Api_GestionFC.DTO
{
    public class AlertaSinSaldoVirtualDTO
    {
        public Models.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public int Cantidad { get; set; }
        public List<AlertaSinSaldoVirtual> ResultDatos { get; set; }

        public AlertaSinSaldoVirtualDTO()
        {
            this.ResultadoEjecucion = new ResultadoEjecucion();
            this.ResultDatos = new List<AlertaSinSaldoVirtual>();
        }
    }
}
