using System.Collections.Generic;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.ObjectValue;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Services.Factories;

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
        public int Dano { get; set; }
        public int QtdMagias { get; set; }
        public GatilhoDeAcao GatilhoDeAcao { get; set; }
        public IAbrirFechaduraService Acao { get; }
        public IPersonagem Personagem { get; }

        public void Build()
        {
            GatilhoDeAcao = GatilhoDeAcao.AbrirFechadura;
            Pv = 2;
            Nome = "Chaveiro";
            Vantagem = "Não gasta tochas ao Abrir Fechaduras.";
            ArmaInicial = new Arma();
            ArmaInicial.Build("Adaga", -1);
            QtdMagias = 0;
        }

        public IAcao AplicaEfeito(IAcao acao)
        {
            if (acao.GatilhoDeAcao == this.GatilhoDeAcao)
            {
                acao.Efeito = () => Efeito(acao);
                return acao;
            }
            return acao;
        }

        public IEnumerable<ActionResult> Efeito(IAcao acao, int? indice = null)
        { IAcaoPorta acaoPorta = (IAcaoPorta)acao;
            IPortaComum porta = acaoPorta.Porta;
            porta.AbrirFechadura();
            porta.SegmentoAlvo = porta.SegmentoAlvo ?? SegmentoFactory.GeraSegmento(porta, indice ?? D6.Rolagem(deslocamento: true));
            BaseSegmento novoSegmento = porta.SegmentoAlvo;
            string texto = string.Empty;
            texto += $"\n  Com as habilidade de CHAVEIRO, {acao.Personagem.Nome} destranca a fechadura rapidamente.";
            ActionResult consequencia = new DungeonConsequence()
            {
                Descricao = texto,
                Segment = novoSegmento
            };

            IEnumerable<ActionResult> result = new List<ActionResult>() { consequencia };
            return result;
        }
    }
}
