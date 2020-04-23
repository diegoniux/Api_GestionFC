using System;
namespace Api_GestionFC.Models
{
    public class ProductividadSemanalDatos
    {
        public string NombreCompletoAP { get; set; }
        public int SaldoVirtualSemana1 { get; set; }
        public int SaldoVirtualSemana2 { get; set; }
        public int SaldoVirtualSemana3 { get; set; }
        public int SaldoVirtualSemana4 { get; set; }
        public string IndicadorSaldoMetaSem1 { get; set; }
        public string IndicadorSaldoMetaSem2 { get; set; }
        public string IndicadorSaldoMetaSem3 { get; set; }
        public string IndicadorSaldoMetaSem4 { get; set; }
        public int SaldoVirtualTetrasemana { get; set; }
        public string IndicadorSaldoMetaTetra { get; set }

        public ProductividadSemanalDatos()
        {
        }
    }
}
