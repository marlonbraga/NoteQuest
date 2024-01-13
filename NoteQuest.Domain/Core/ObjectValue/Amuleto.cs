using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Inventario;
using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;

namespace NoteQuest.Domain.Core.ObjectValue
{
    public class Amuleto : IAmuleto
    {
        public IPontosDeVida Pv { get; set; }
        public IEncantamento Encantamento { get; private set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool EstaAmaldicoado { get; set; }

        public Amuleto(string nome, int pontosDeVida)
        {
            Nome = nome;
            Pv = new PontosDeVida(pontosDeVida);
        }

        public void DefinirEncantamento(IEncantamento encantamento)
        {
            Encantamento = encantamento;
            Nome = encantamento.Nome.Replace("[Armadura]", Nome);
            Descricao = encantamento.Descricao;
        }

        public IEvent EffectSubstitutionComposite(IEvent gameEvent)
        {
            if (gameEvent?.GetType().Name == Encantamento.EventTrigger)
                gameEvent.Efeito = () => Encantamento.Efeito(gameEvent);

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
