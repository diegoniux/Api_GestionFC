using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class SolicitudRegistroTraspaso
    {
        public int RegistroTraspasoId { get; set; }
        public string FolioSolicitud { get; set; }
        public int EstatusId { get; set; }
        public string EstatusDescripcion { get; set; }
        public int Seccion { get; set; }
        public string Porcentaje { get; set; }
        public string ImgDocumental { get; set; }
        public string ImgInvestigacion { get; set; }
        public string ImgProcesar { get; set; }
        public string ImgSaldoVirtual { get; set; }
        public string TextDocumental { get; set; }
        public string TextInvestigacion { get; set; }
        public string TextProcesar { get; set; }
        public string TextSaldoVirtual { get; set; }
        public string ColorDocumental { get; set; }
        public string ColorInvestigacion { get; set; }
        public string ColorProcesar { get; set; }
        public string ColorSaldoVirtual { get; set; }
    }
}
