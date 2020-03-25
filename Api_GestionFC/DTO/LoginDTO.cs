using Api_GestionFC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.DTO
{
    public class LoginDTO
    {
        public Usuario Usuario { get; set; }
        public ResultadoEjecucion ResultadoEjecucion { get; set; }
        public string Token { get; set; }

    }
}
