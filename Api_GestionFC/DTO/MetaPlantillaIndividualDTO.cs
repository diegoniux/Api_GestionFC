using Api_GestionFC.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.DTO
{
    public class MetaPlantillaIndividualDTO
    {
        public ResultadoEjecucion  ResultadoEjecucion { get; set; }
        public String ComisionEstimada { get; set; }
        public MetaAP MetaAP { get; set; }

        public MetaPlantillaIndividualDTO()
        {
            this.ResultadoEjecucion = new ResultadoEjecucion();
            this.ComisionEstimada = "$0";
            this.MetaAP = new MetaAP();
        }


    }
}
