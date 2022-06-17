using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core.DTO
{
    public class ConsequenciaDTO
    {
        public string Descricao { get; set; }
        public ISegmento Segmento { get; set; }
        public List<IEscolha> Escolhas { get; set; }
    }
}
