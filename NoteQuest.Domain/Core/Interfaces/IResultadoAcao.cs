using System.Collections.Generic;

namespace NoteQuest.Domain.Core.Interfaces
{
    public interface IResultadoAcao
    {
        public string Descrição { get; set; }
        public List<IAcao> AcoesPossiveis { get; set; }
    }
}
