using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class AbrirUmBau : IEvent
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public IPersonagem Personagem { get; set; }
        public Func<IEnumerable<ActionResult>> Efeito { get; set; }
        public string EventTrigger { get; set; }
        public IDictionary<string, IEvent> ChainedEvents { get; set; }

        public AbrirUmBau()
        {
            EventTrigger = nameof(AbrirUmBau);
            Efeito = delegate { return Executar(); };
            Descricao = "Encontra moedas e tesouros; Raramente aciona armadilhas.";
            Titulo = "Abrir Baú";
        }

        public IEnumerable<ActionResult> Executar(int? indice = null)
        {
            return null;
        }
    }
}
