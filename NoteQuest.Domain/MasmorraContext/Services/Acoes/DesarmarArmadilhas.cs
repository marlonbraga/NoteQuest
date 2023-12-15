using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class DesarmarArmadilhas : IEvent
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public string EventTrigger { get; set; }
        public IDictionary<string, IEvent> ChainedEvents { get; set; }
        public IPersonagem Personagem { get; set; }
        public Func<IEnumerable<ActionResult>> Efeito { get; set; }

        public DesarmarArmadilhas()
        {
            EventTrigger = nameof(DesarmarArmadilhas);
            Efeito = delegate { return Executar(); };
            Titulo = "Desarmar armadilhas";
            Descricao = "Torna sala segura quanto a armadilhas. Ação demorada. Gasta 1 tocha";
        }

        public IEnumerable<ActionResult> Executar(int? indice = null)
        {
            return null;
        }
    }
}
