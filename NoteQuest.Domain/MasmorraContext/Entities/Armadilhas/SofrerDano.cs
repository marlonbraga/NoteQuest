using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities.Armadilhas
{
    public class SofrerDano : IEvent
    {
        public string Descricao { get; set; }
        public int Dano { get; set; }
        public string Titulo { get; set; }
        public string EventTrigger { get; set; }
        public IPersonagem Personagem { get; set; }
        public Func<IEnumerable<ActionResult>> Efeito { get; set; }
        public IDictionary<string, IEvent> ChainedEvents { get; set; }

        public SofrerDano(string descricao, int dano)
        {
            Efeito = delegate { return Executar(); };
            Descricao = descricao;
            Dano = dano;
        }

        public IEnumerable<ActionResult> Executar()
        {
            string texto = $"\n  {Descricao}";
            texto += $"\n  {Personagem.Nome} sofreu {Dano} de dano!";
            Personagem.Pv.ReceberDano(Dano, out bool morreu);
            if (morreu)
            {
                texto += $"\n  {Personagem.Nome} morreu!";
            }
            ActionResult consequencia = new (texto);
            IEnumerable<ActionResult> result = new List<ActionResult>() { consequencia };

            return result;
        }
    }
}
