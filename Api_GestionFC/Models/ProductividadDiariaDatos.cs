using System;
namespace Api_GestionFC.Models
{
    public class ProductividadDiariaDatos
    {
        public string NombreCompletoAP { get; set; }
        public int SaldoVirtualLunes { get; set; }
        public int SaldoVirtualMartes { get; set; }
        public int SaldoVirtualMiercoles { get; set; }
        public int SaldoVirtualJueves { get; set; }
        public int SaldoVirtualViernes { get; set; }
        public int SaldoVirtualSabado { get; set; }
        public int SaldoVirtualDomingo { get; set; }
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
