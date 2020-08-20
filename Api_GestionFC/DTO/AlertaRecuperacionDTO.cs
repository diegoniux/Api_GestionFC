using Api_GestionFC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.DTO
{
    public class AlertaRecuperacionDTO
    {
        public Models.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public int Cantidad { get; set; }
        public List<AlertaRecuperacion> ResultDatos { get; set; }
        public List<AlertaRecuperacionPantallas> Pantallas { get; set; }
        public List<AlertaRecuperacionPreguntas> Preguntas { get; set; }
        public List<AlertaRecuperacionDocumentos> Documentos { get; set; }

        public AlertaRecuperacionDTO()
        {
            this.ResultadoEjecucion = new ResultadoEjecucion();
            this.ResultDatos = new List<AlertaRecuperacion>();
            this.Pantallas = new List<AlertaRecuperacionPantallas>();
            this.Preguntas = new List<AlertaRecuperacionPreguntas>();
            this.Documentos = new List<AlertaRecuperacionDocumentos>();
        }

    }
}
