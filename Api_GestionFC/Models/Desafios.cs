using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Api_GestionFC.Models
{
    public class Desafios
    {
        public int BonoTableta { get; set; }
        public string ImgBonoTableta { get; set; }
        public string ColorBonoTableta { get; set; }
        public int Multiplicador { get; set; }
        public string ImgMultiplicador { get; set; }
        public string ColorMultiplicador { get; set; }
        public int BonoBisemanal { get; set; }
        public string ImgBonoBisemanal { get; set; }
        public string ColorBonoBisemanal { get; set; }
        public int Prospectos { get; set; }
        public string ImgProspectos { get; set; }
        public string ColorProspectos { get; set; }
        public int MetaComercial { get; set; }
        public string ImgMetaComercial { get; set; }
        public string ColorMetaComercial { get; set; }
        public int SemanasMeta { get; set; }
        public string ImgSemanasMeta { get; set; }
        public string ColorSemanasMeta { get; set; }
    }
}
