using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Domain.MasmorraContext.DTO
{
    public class MasmorraNomesDTO : IMasmorraNomes
    {
        public Tipodemasmorra[] TipoDeMasmorra { get; set; }
        public Segundaparte[] SegundaParte { get; set; }
        public Terceiraparte[] TerceiraParte { get; set; }
    }

    public class Tipodemasmorra
    {
        public int indice { get; set; }
        public string tipo { get; set; }
    }
    public class Segundaparte
    {
        public int indice { get; set; }
        public string nome { get; set; }
    }
    public class Terceiraparte
    {
        public int indice { get; set; }
        public string nome { get; set; }
    }
}
