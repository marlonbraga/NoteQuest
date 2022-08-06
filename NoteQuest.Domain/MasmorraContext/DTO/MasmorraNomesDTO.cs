using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.DTO
{
    public class MasmorraNomesDTO : IMasmorraNomes
    {
        public IDictionary<ushort, Tipodemasmorra> TipoDeMasmorra { get; set; }
        public IDictionary<ushort, Segundaparte> SegundaParte { get; set; }
        public IDictionary<ushort, Terceiraparte> TerceiraParte { get; set; }
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
