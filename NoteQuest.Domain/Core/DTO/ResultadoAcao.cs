using NoteQuest.Domain.Core.Interfaces;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core.DTO
{
    public class ResultadoAcao : IResultadoAcao
    {
        public string Descrição { get; set; }
        public List<IAcao> AcoesPossiveis { get; set; }
    }
}
