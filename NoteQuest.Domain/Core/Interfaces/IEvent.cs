using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core.Interfaces
{
    public enum AcaoTipo
    {
        PortaFrente,
        PortaDireita,
        PortaTras,
        PortaEsquerda,
        Segmento,
        Batalha,
        Nulo = 99
    }

    public interface IEvent
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string EventTrigger { get; set; }
        public IPersonagem Personagem { get; set; }
        public Func<IEnumerable<ActionResult>> Efeito { get; set; }
        public IDictionary<string, IEvent> ChainedEvents { get; set; }
    }
}
