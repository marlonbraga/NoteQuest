using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using System;
using System.Collections.Generic;
using NoteQuest.Domain.Core;

namespace NoteQuest.Domain.MasmorraContext.Entities.Armadilhas
{
    public class Lamina : IEvent
    {
        public string Descricao { get; set; }
        public string Titulo { get; set; }
        public string EventTrigger { get; set; }
        public IPersonagem Personagem { get; set; }
        public Func<IEnumerable<ActionResult>> Efeito { get; set; }
        public IDictionary<string, IEvent> ChainedEvents { get; set; }

        public Lamina(string descricao)
        {
            Efeito = delegate { return Executar(); };
            Descricao = descricao;
            ChainedEvents = new Dictionary<string, IEvent>();
            ChainedEvents["Decapitacao"] = new Decapitacao(string.Empty);
            ChainedEvents["Amputacao"] = new Amputacao(string.Empty);
        }

        public IEnumerable<ActionResult> Executar(int? index = null)
        {
            List<ActionResult> result = new();

            string texto = $"\n  {Descricao}";

            ActionResult consequencia = new(texto);
            result.Add(consequencia);

            index ??= D6.Rolagem();
            if (index == 1)
            {
                result.AddRange(ChainedEvents["Decapitacao"].Efeito.Invoke());
            }
            else if (index == 2)
            {
                result.AddRange(ChainedEvents["Amputacao"].Efeito.Invoke());
            }
            else
            {
                string desvioDescicao = $"\n  Porém, {Personagem.Nome} consegue desviar a tempo e não sofre dano algum";
                ActionResult desvio = new(desvioDescicao);
                result.Add(desvio);
            }
            
            return result;
        }
    }
}
