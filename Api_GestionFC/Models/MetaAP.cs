using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class MetaAP
    {
        public int IdDetalleMetaSaldoAcumuladoAP { get; set; }
        public int Nomina { get; set; }
        public string Foto { get; set; }
        public bool EsFrontera { get; set; }
        public bool EsNovato { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string SaldoMeta { get; set; }
        public string ComisionEstimada { get; set; }
    }
}
