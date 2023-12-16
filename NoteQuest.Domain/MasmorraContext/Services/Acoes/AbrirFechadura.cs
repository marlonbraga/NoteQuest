using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Services.Factories;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class AbrirFechadura : IAcaoPorta
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public IMasmorra Masmorra { get; set; }
        public int? IndicePreDefinido { get; set; }
        public IPortaComum Porta { get; set; }
        public IPersonagem Personagem { get; set; }
        public string EventTrigger { get; set; }
        public Func<IEnumerable<ActionResult>> Efeito { get; set; }
        public IDictionary<string, IEvent> ChainedEvents { get; set; }

        public AbrirFechadura(IPortaComum porta, int? indicePreDefinido = null)
        {
            Porta = porta;
            Titulo = $"Abrir fechadura {porta.Posicao}";
            Descricao = "Arrombar fechadura silenciosamente. Ação demorada.";
            IndicePreDefinido = indicePreDefinido;
            Masmorra = porta.Masmorra;
            Efeito = delegate { return Executar(); };
        }

        public IEnumerable<ActionResult> Executar(int? indice = null)
        {
            //TODO: Evento de escuridão (remover 1 tocha ou encerrar o eeita da magia LUZ)
            Porta.AbrirFechadura();
            Porta.SegmentoAlvo ??= Porta.SegmentoAtual.Masmorra.SegmentoFactory.GeraSegmento(Porta, IndicePreDefinido ?? indice ?? D6.Rolagem(deslocamento: true));
            string texto = string.Empty;
            texto += $"\n  Você gasta algum tempo tentando arrombar o cadeado. A porta é destravada revelando um segmento da masmorra.";
            texto += $"\n  Porém o processo foi demorado. A iluminação cessou te colocando outra vez na escuridão.";
            DungeonConsequence consequencia = new(texto, Porta.SegmentoAtual);
            //TODO: Verifica se é uma sala recem criada e passa a Escolha de gerar Conteudo e Monstros

            IEnumerable<ActionResult> result = new List<ActionResult>() { consequencia };
            return result;
        }
    }
}
