using System.Collections.Generic;
using NoteQuest.Domain.MasmorraContext.DTO;

namespace NoteQuest.Domain.MasmorraContext.Interfaces
{
    public interface IMasmorraNomes
    {
        public IList<string> TipoDeMasmorra { get; set; }
        public IList<string> SegundaParte { get; set; }
        public IList<string> TerceiraParte { get; set; }
    }
}
