using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core.DTO
{
    public class ConsequenciaDTO
    {
        public string Descricao { get; set; }
        public BaseSegmento Segmento { get; set; }
        public List<IEscolha> Escolhas { get; set; }
    }
}
