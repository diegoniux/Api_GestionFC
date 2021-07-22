using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class MetaPlantilla
    {
        public int IdMetaSaldoAcumuladoGerenteIndividual { get; set; }
        public bool ExisteConfiguracionIndividual { get; set; }
        public string SaldoAcumuladoMeta { get; set; }
        public string SaldoAcumuladoTetra { get; set; }
        public int Traspasos { get; set; }
        public int TraspasosFCT { get; set; }
        public string ComisionSem { get; set; }
        public string BonoDesarrollo { get; set; }
        public string BonoExcelencia { get; set; }
        public int SemanaTetraSemana { get; set; }
        public int MaxSemanas { get; set; }
        public string TotalTetra { get; set; }

    }
}
