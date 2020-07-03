using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class MetaPlantillaSaldoAcumulado
    {
        public int IdMetaSaldoAcumuladoGerenteIndividual { get; set; }
        public int IdPeriodo { get; set; }
        public int Nomina { get; set; }
        public int SaldoAcumuladoMeta { get; set; }
    }
}
