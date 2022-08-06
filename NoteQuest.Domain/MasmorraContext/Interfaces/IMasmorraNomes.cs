using NoteQuest.Domain.MasmorraContext.DTO;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Interfaces.Dados
{
    public interface IMasmorraNomes
    {
        public IDictionary<ushort, Tipodemasmorra> TipoDeMasmorra { get; set; }
        public IDictionary<ushort, Segundaparte> SegundaParte { get; set; }
        public IDictionary<ushort, Terceiraparte> TerceiraParte { get; set; }
    }
}
