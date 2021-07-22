using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class DetalleMetaPorDia
    {
        public int SaldoLunes { get; set; }
        public int SaldoMartes { get; set; }
        public int SaldoMiercoles{ get; set; }
        public int SaldoJueves { get; set; }
        public int SaldoViernes { get; set; }
        public int SaldoSabado { get; set; }
        public int SaldoDomingo { get; set; }
    }
}
