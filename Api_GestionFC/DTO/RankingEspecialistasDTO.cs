using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.DTO
{
    public class RankingEspecialistasDTO
    {
        public Models.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public List<Models.RankingEspecialista> TopEspecialistas { get; set; }
        public List<Models.RankingEspecialista> Especialistas { get; set; }
        public string ImgPosicionSemAntNacional { get; set; }

        public RankingEspecialistasDTO()
        {
            this.Especialistas = new List<Models.RankingEspecialista>();
            this.TopEspecialistas = new List<Models.RankingEspecialista>();
            this.ResultadoEjecucion = new Models.ResultadoEjecucion();
        }
    }
}
