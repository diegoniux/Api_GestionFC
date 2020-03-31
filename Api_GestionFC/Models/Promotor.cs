using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class Promotor
    {
        public int Nomina { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Foto { get; set; }
        public string FotoColor { get; set; }
        public string PorcentajeMeta { get; set; }
        public string PorcentajeMetaColor { get; set; }
    }
}
