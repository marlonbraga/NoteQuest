using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Inventario;
using System.Collections.Generic;

namespace NoteQuest.Domain.ItensContext.ObjectValue.Encantamentos
{
    public class DaDestruicao : IEncantamento
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string EventTrigger { get; set; }

        public DaDestruicao(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
            //EventTrigger = nameof(Ataque);
        }

        public IEnumerable<ActionResult> Efeito(IEvent acao, int? indice = null)
        {
            //TODO: (Causa +2 de dano)
            throw new System.NotImplementedException();
        }

        public IEvent EffectSubstitutionComposite(IEvent gameEvent)
        {
            if (gameEvent?.GetType().Name == EventTrigger)
                gameEvent.Efeito = () => Efeito(gameEvent);

            if (gameEvent?.ChainedEvents is not null)
                foreach (var subEvent in gameEvent?.ChainedEvents)
                {
                    if (subEvent.Value is not null)
                    {
                        subEvent.Value.Personagem = gameEvent.Personagem;
                        EffectSubstitutionComposite(subEvent.Value);
                    }
                }

            return gameEvent;
        }
    }
}
