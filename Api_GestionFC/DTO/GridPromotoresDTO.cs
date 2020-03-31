﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models = Api_GestionFC.Models;

namespace Api_GestionFC.DTO
{
    public class GridPromotoresDTO
    {
        public Models.ResultadoEjecucion ResultadoEjecucion { get; set; }
        public List<Models.Promotor> Promotores { get; set; }
    }

}