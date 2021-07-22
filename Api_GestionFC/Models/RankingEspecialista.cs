using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class RankingEspecialista : Especialista
    {
        public string Posicion { get; set; }
        public string TipoSaldo { get; set; }
        public string ImgPosicionSemAnt { get; set; }
        public string ColorPosicion { get; set; }
        public string ColorTextoSaldo { get; set; }
        public RankSemanal Monedas { get; set; }

        public RankingEspecialista() {
            this.Monedas = new RankSemanal();
        }
    }
}
