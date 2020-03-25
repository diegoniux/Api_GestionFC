using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class ResultadoEjecucion
    {
        public bool EjecucionCorrecta { get; set; }
        public string ErrorMessage{ get; set; }
        public string FriendlyMessage { get; set; }

    }
}
