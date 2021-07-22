using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class AlertaSinSaldoVirtual
    {
        public int IdAlerta { get; set; }
        public int IdTipoAlerta { get; set; }
        public int IdEstatusAlerta { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Folio { get; set; }
        public string SaldoVirtual { get; set; }
        public string TipoSolicitud { get; set; }
        public string FechaFirma { get; set; }
        public bool TieneSV { get; set; }
        public string FechaActivacionFCT { get; set; }
    }
}
