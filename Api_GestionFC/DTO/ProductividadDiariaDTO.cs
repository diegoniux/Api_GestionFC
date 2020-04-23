using System;
using System.Collections.Generic;
using Models = Api_GestionFC.Models;

namespace Api_GestionFC.DTO
{
    public class ProductividadDiariaDTO
    {
        public Models.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public Models.ProductividadFechas ResultFechas { get; set; }
        public List<Models.ProductividadDatos> ResultDatos { get; set; }
        public Models.ProductividadAnioSemana ResultAnioSemana { get; set; }

        public ProductividadDiariaDTO()
        {
            this.ResultadoEjecucion = new Models.ResultadoEjecucion();
            this.ResultAnioSemana = new Models.ProductividadAnioSemana();
            this.ResultDatos = new List<Models.ProductividadDatos>();
            this.ResultFechas = new Models.ProductividadFechas();
        }
    }
}
