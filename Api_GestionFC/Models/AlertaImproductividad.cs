using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class AlertaImproductividad
    {
        public string Foto { get; set; }
        public int IdAlerta { get; set; }
        public int IdTipoAlerta { get; set; }
        public int IdEstatusAlerta { get; set; }
        public int NominaAP { get; set; }
        public string NombreAP { get; set; }
        public string ApellidosAP { get; set; }
        public int DiasSinFolios { get; set; }
        public int DiasRestantes { get; set; }
        public string Msj1 { get; set; }
        public string Msj2 { get; set; }
        public string Msj3 { get; set; }
        public bool BanderaCalendar { get; set; }
        public string ColorCalendar { get; set; }
        public string MsjEstatus { get; set; }
        public string ImgNotificacion { get; set; }
        public bool ImgWarning { get; set; }

        public AlertaImproductividad()
        {

        }
    }
}
