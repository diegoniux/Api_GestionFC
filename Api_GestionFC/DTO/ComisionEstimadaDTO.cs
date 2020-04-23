using System;
namespace Api_GestionFC.DTO
{
    public class ComisionEstimadaDTO
    {
        public Models.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public string ComisionEstimada { get; set; }
        public string BonoExcelenciaEstimado { get; set; }

        public ComisionEstimadaDTO()
        {
            this.ResultadoEjecucion = new Models.ResultadoEjecucion();
        }
    }
}
