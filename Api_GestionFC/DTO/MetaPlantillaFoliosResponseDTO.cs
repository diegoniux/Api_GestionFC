using Api_GestionFC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.DTO
{
    public class MetaPlantillaFoliosResponseDTO
    {
        public ResultadoEjecucion ResultadoEjecucion { get; set; }
        public int IdMetaSaldoAcumuladoGerenteIndividual { get; set; }

        public MetaPlantillaFoliosResponseDTO()
        {
            this.ResultadoEjecucion = new ResultadoEjecucion();
            this.IdMetaSaldoAcumuladoGerenteIndividual = 0;
        }
    }
}
