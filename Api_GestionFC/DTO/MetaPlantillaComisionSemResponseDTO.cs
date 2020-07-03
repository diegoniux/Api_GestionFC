using Api_GestionFC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.DTO
{
    public class MetaPlantillaComisionSemResponseDTO
    {
        public ResultadoEjecucion ResultadoEjecucion { get; set; }
        public int IdMetaSaldoAcumuladoGerenteIndividual { get; set; }
        public int SaldoAcumuladoMeta { get; set; }

        public MetaPlantillaComisionSemResponseDTO()
        {
            this.ResultadoEjecucion = new ResultadoEjecucion();
            this.IdMetaSaldoAcumuladoGerenteIndividual = 0;
            this.SaldoAcumuladoMeta = 0;
        }
    }
}
