using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Inventario;
using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;

namespace NoteQuest.Domain.Core.ObjectValue
{
    public class Arma : IArma
    {
        public bool EstaAmaldicoado { get; set; }
        public IPontosDeVida Pv { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool EmpunhaduraDupla { get; set; }
        public short Dano { get; set; }
        public IEncantamento Encantamento { get; private set; }

        public void Build(string nome, short dano = 0, bool empunhaduraDupla = false, bool maldicao = false)
        {
            Nome = nome;
            EmpunhaduraDupla = empunhaduraDupla;
            string empunhadura = empunhaduraDupla? $"(Duas mãos)" : string.Empty;
            Descricao = $"Dano: {FormataDano(dano)} {empunhadura}";
            Dano = dano;
            EstaAmaldicoado = maldicao;
        }

        public void DefinirEncantamento(IEncantamento encantamento)
        {
            Encantamento = encantamento;
            Nome = encantamento.Nome.Replace("[Arma]", Nome);
            Descricao += $" ({encantamento.Descricao})";
        }

        private string FormataDano(short dano)
        {
            if (dano < 0) return $"1D6{dano}";
            if (dano > 0) return $"1D6+{dano}";
            return $"1D6";
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
