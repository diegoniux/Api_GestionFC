using System;
namespace Api_GestionFC.Models
{
    public class ProductividadTotal
    {
        public int SaldoVirtualTotal { get; set; }
        public int Anio { get; set; }
        public int TetrasemanaAnio { get; set; }
        public bool EsActual { get; set; }
        public DateTime FechaCorte { get; set; }
        public bool EsUltimaFechaCorte { get; set; }

        public ProductividadTotal()
        {
        }
    }
}
