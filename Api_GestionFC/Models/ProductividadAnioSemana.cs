using System;
namespace Api_GestionFC.Models
{
    public class ProductividadAnioSemana
    {
        public int Anio { get; set; }
        public int SemanaAnio { get; set; }
        public bool EsActual { get; set; }
        public DateTime FechaCorte { get; set; }
        public bool EsUltimaFechaCorte { get; set; }
        public int TotalSaldoVirtualLunes { get; set; }
        public int TotalSaldoVirtualMartes { get; set; }
        public int TotalSaldoVirtualMiercoles { get; set; }
        public int TotalSaldoVirtualJueves { get; set; }
        public int TotalSaldoVirtualViernes { get; set; }
        public int TotalSaldoVirtualSabado { get; set; }
        public int TotalSaldoVirtualDomingo { get; set; }
        public int TotalSaldoVirtualSemana { get; set; }
        public int TotalSaldoVirtualFaltante { get; set; }
        public int TotalMetaSemana { get; set; }
        public int TotalFCTInactivos { get; set; }
        public int TotalFCTActivos { get; set; }
        public int TotalFoliosMenores30k { get; set; }
        public int TotalFoliosCertificados { get; set; }

        public ProductividadAnioSemana()
        {
        }
    }
}
