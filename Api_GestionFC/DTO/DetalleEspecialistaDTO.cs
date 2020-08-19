using Api_GestionFC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.DTO
{
    public class DetalleEspecialistaDTO
    {
        public Models.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public DetalleEspecialista detalleEspecialista { get; set; }
        public Desafios desafios { get; set; }

        public DetalleEspecialistaDTO()
        {
            this.ResultadoEjecucion = new ResultadoEjecucion();
            this.detalleEspecialista = new DetalleEspecialista();
            this.desafios = new Desafios();
        }
    }
}
