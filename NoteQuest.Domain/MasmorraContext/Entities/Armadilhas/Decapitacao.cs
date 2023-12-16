using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities.Armadilhas
{
    public class Decapitacao : IEvent
    {
        public string Descricao { get; set; }
        public string Titulo { get; set; }
        public string EventTrigger { get; set; }
        public IPersonagem Personagem { get; set; }
        public Func<IEnumerable<ActionResult>> Efeito { get; set; }
        public IDictionary<string, IEvent> ChainedEvents { get; set; }

        public Decapitacao(string descricao)
        {
            Efeito = delegate { return Executar(); };
            Descricao = descricao;
        }

        public IEnumerable<ActionResult> Executar()
        {
            string texto = $"\n  {Descricao}";
            Personagem.Pv.ReceberDano(Personagem.Pv.Pv, out bool morreu);
            texto += $"\n  {Personagem.Nome} vê o mundo girar e num instante para no chão. Não consegue mais sentir seu corpo. Mas o vê delogado a sua frente caindo e se juntando ao chão gelado da masmorra. Nada mais se vê. Nada mais se sente.";
            texto += $"\n  {Personagem.Nome} morreu!";
            //TODO: Adicionar evento de morte ao ChainedEvents
            ActionResult consequencia = new (texto);
            IEnumerable<ActionResult> result = new List<ActionResult>() { consequencia };

            return result;
        }
    }
}
