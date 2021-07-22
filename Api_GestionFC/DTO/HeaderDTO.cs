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
        public int Plantilla { get; set; }
        public int APsMetaAlcanzada { get; set; }
        public string Perfil { get; set; }
        public Models.Progreso Progreso { get; set; }        

        public HeaderDTO() 
        {
            this.ResultadoEjecucion = new Models.ResultadoEjecucion();
            this.Progreso = new Models.Progreso();
        }
    }
}
