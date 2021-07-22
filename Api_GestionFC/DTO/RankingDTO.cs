using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.DTO
{
    public class RankingDTO
    {
        public Models.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public List<Models.RankingGte> TopGerentes { get; set; }
        public List<Models.RankingGte> Gerentes { get; set; }
        public int PosicionDireccion { get; set; }
        public string ImgPosicionSemAntDireccion { get; set; }
        public int PosicionNacional { get; set; }
        public string ImgPosicionSemAntNacional { get; set; }

        public RankingDTO() {
            this.Gerentes = new List<Models.RankingGte>();
            this.TopGerentes = new List<Models.RankingGte>();
            this.ResultadoEjecucion = new Models.ResultadoEjecucion();
        }
    }
}
