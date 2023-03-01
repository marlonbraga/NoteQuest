using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using System.Collections.Generic;

namespace NoteQuest.Application
{
    public class ConsequenciaView
    {
        public string Descricao { get; set; }
        public ISegmento Segmento { get; set; }
        public IDictionary<int, EscolhaView> Escolhas { get; set; }
    }

    public class EscolhaView
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
    }
}
