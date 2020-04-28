using Api_GestionFC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.DTO
{
    public class CatalogoDTO
    {
        public string Clave { get; set; }
        public string Valor { get; set; }
        public ResultadoEjecucion ResultadoEjecucion { get; set; }

        public CatalogoDTO()
        {
            this.ResultadoEjecucion = new Models.ResultadoEjecucion();
        }

    }
}
