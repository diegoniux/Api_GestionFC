using Api_GestionFC.Models;

namespace Api_GestionFC.DTO
{
    public class MetaPlantillaDTO
    {
        public ResultadoEjecucion ResultadoEjecucion { get; set; }
        public MetaPlantilla MetaPlantilla { get; set; }
        public FechasSemana FechasSemana { get; set; }
        public  DetalleMetaPorDia DetalleMetaPorDia { get; set; }

        public MetaPlantillaDTO()
        {
            this.ResultadoEjecucion = new ResultadoEjecucion();
            this.MetaPlantilla = new MetaPlantilla();
            this.FechasSemana = new FechasSemana();
            this.DetalleMetaPorDia = new DetalleMetaPorDia();
        }
    }
}
