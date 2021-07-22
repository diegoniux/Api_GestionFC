using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class DetalleEspecialista
    {
        public int NominaPromotor { get; set; }
        public string Foto { get; set; }
        public string Genero { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string MesesLaborando { get; set; }
        public string SaldoAcumulado { get; set; }
        public string SaldoMeta { get; set; }
        public string PorcentajeSaldoAcumulado { get; set; }
        public string ImagenSaldoAcumulado { get; set; }
        public int NivelComision { get; set; }
        public int TramitesPorRecuperar { get; set; }
        public int TramitesEnCalidad { get; set; }

    }
}
