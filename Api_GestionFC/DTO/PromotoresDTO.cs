﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models = Api_GestionFC.Models;

namespace Api_GestionFC.DTO
{
    public class PromotoresDTO
    {
        public Models.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public List<Models.Progreso> Promotores { get; set; }

        public PromotoresDTO()
        {
            this.ResultadoEjecucion = new Models.ResultadoEjecucion();
            this.Promotores = new List<Models.Progreso>();
        }
    }

}
