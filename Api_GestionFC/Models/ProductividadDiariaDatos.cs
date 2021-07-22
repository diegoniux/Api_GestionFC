using System;
namespace Api_GestionFC.Models
{
    public class ProductividadDiariaDatos
    {
        public string NombreCompletoAP { get; set; }
        public string SaldoVirtualLunes { get; set; }
        public string SaldoVirtualMartes { get; set; }
        public string SaldoVirtualMiercoles { get; set; }
        public string SaldoVirtualJueves { get; set; }
        public string SaldoVirtualViernes { get; set; }
        public string SaldoVirtualSabado { get; set; }
        public string SaldoVirtualDomingo { get; set; }
        public int SaldoVirtualSemana { get; set; }
        public string IndicadorSaldoMeta { get; set; }
        public int SaldoVirtualFaltante { get; set; }
        public int MetaSemana { get; set; }
        public int FCTInactivos { get; set; }
        public int FCTActivos { get; set; }
        public int FoliosMenores30k { get; set; }
        public int FoliosCertificados { get; set; }

        public ProductividadDiariaDatos()
        {
        }
    }
}
