using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.Core.ObjectValue;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core.Classes
{
    public class Chaveiro : IClasse
    {
        public int Indice { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Pv { get; set; }
        public string Vantagem { get; set; }
        public IArma ArmaInicial { get; set; }
        public int QtdMagias { get; set; }
        public string EventTrigger { get; set; }

        public void Build()
        {
            EventTrigger = nameof(AbrirFechadura);
            Pv = 2;
            Nome = "Chaveiro";
            Vantagem = "Não gasta tochas ao Abrir Fechaduras.";
            ArmaInicial = new Arma();
            ArmaInicial.Build("Adaga", -1);
            QtdMagias = 0;
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

        public IEnumerable<ActionResult> Efeito(IEvent acao, int? indice = null)
        {
            IAcaoPorta acaoPorta = (IAcaoPorta)acao;
            IPortaComum porta = acaoPorta.Porta;
            porta.AbrirFechadura();
            porta.SegmentoAlvo ??= porta.SegmentoAtual.Masmorra.SegmentoFactory.GeraSegmento(porta, indice ?? D6.Rolagem(deslocamento: true));
            BaseSegmento novoSegmento = porta.SegmentoAlvo;
            string texto = string.Empty;
            texto += $"\n  Com as habilidade de CHAVEIRO, {acao.Personagem.Nome} destranca a fechadura rapidamente.";
            ActionResult consequencia = new DungeonConsequence(texto, novoSegmento);
            IEnumerable<ActionResult> result = new List<ActionResult>() { consequencia };

            return result;
        }
    }
}
