using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities.Armadilhas
{
    public class Click : IEvent
    {
        public string Descricao { get; set; }
        public string Titulo { get; set; }
        public string EventTrigger { get; set; }
        public IPersonagem Personagem { get; set; }
        public Func<IEnumerable<ActionResult>> Efeito { get; set; }
        public IDictionary<string, IEvent> ChainedEvents { get; set; }

        public Click(string descricao)
        {
            Efeito = delegate { return Executar(); };
            Descricao = descricao;
        }

        public IEnumerable<ActionResult> Executar()
        {
            string texto = $"\n  {Descricao}";
            ActionResult consequencia = new (texto);
            IEnumerable<ActionResult> result = new List<ActionResult>() { consequencia };

            return result;
        }
    }
}
