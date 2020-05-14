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

        public ProductividadAnioSemana()
        {
        }
    }
}
