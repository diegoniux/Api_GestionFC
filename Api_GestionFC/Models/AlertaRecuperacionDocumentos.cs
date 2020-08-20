using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class AlertaRecuperacionDocumentos
    {
        public int Pantalla { get; set; }
        public int ExpedienteTipoId { get; set; }
        public string ExpedienteDesc { get; set; }
        public int DocumentoTipoId { get; set; }
        public string DocumentoDesc { get; set; }
        public string ClaveDocumento { get; set; }
        public int Consecutivo { get; set; }
        public string Mascara { get; set; }
        public string Path { get; set; }
    }
}
