﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class Especialista
    {
        public int Nomina { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Genero { get; set; }
        public string Foto { get; set; }
        public string SaldoVirtual { get; set; }
        public int NumTraspaso { get; set; }
    }
}
