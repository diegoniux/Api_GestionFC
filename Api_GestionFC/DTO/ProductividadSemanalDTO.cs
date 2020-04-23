using System;
using System.Collections.Generic;

namespace Api_GestionFC.DTO
{
    public class ProductividadSemanalDTO
    {
        public Models.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public Models.ProductividadSemanal ResultSemanas { get; set; }
        public List<Models.ProductividadSemanalDatos> ResultDatos { get; set; }
        public Models.ProductividadTotal ResultTotal { get; set; }

        public ProductividadSemanalDTO()
        {
            this.ResultadoEjecucion = new Models.ResultadoEjecucion();
            this.ResultDatos = new List<Models.ProductividadSemanalDatos>();
            this.ResultSemanas = new Models.ProductividadSemanal();
            this.ResultTotal = new Models.ProductividadTotal();
        }
    }
}
