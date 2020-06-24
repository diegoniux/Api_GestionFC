using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.DTO
{
    public class RankingDTO
    {
        public Models.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public List<Models.RankingAP> TopGerentes { get; set; }
        public List<Models.RankingAP> Gerentes { get; set; }
        public int PosicionDireccion { get; set; }
        public string ImgPosicionSemAntDireccion { get; set; }
        public int PosicionNacional { get; set; }
        public string ImgPosicionSemAntNacional { get; set; }

        public RankingDTO() {
            this.Gerentes = new List<Models.RankingAP>();
            this.TopGerentes = new List<Models.RankingAP>();
            this.ResultadoEjecucion = new Models.ResultadoEjecucion();
        }
    }
}
