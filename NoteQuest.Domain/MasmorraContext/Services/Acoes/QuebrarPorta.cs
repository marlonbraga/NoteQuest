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
    public class QuebrarPorta : IEvent
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public IPortaComum Porta { get; set; }
        public IMasmorra Masmorra { get; set; }
        public int? IndicePreDefinido { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public string EventTrigger { get; set; }
        public IDictionary<string, IEvent> ChainedEvents { get; set; }
        public IPersonagem Personagem { get; set; }
        public Func<IEnumerable<ActionResult>> Efeito { get; set; }

        public QuebrarPorta(IPortaComum porta, int? indicePreDefinido)
        {
            EventTrigger = nameof(QuebrarPorta);
            Efeito = delegate { return Executar(); };
            Porta = porta;
            Masmorra = porta.Masmorra;
            Titulo = $"Quebrar porta {porta.Posicao}";
            Descricao = "Abre porta rapidamente. Alerta monstros.";
            IndicePreDefinido = indicePreDefinido;
        }

        public IEnumerable<ActionResult> Executar(int? indice = null)
        {
            Porta.QuebrarPorta();
            Porta.SegmentoAlvo ??= Porta.SegmentoAtual.Masmorra.SegmentoFactory.GeraSegmento(Porta, IndicePreDefinido ?? indice ?? D6.Rolagem(deslocamento: true));
            BaseSegmento novoSegmento = Porta.SegmentoAlvo;
            string texto = string.Empty;
            texto += $"\n  {Personagem?.Nome} aplica diversos golpes a porta. O barulho ecoa pelo ambinete. A porta logo é quebrada revelando um segmento da masmorra.";
            DungeonConsequence consequencia = new(texto, novoSegmento);

            IEnumerable<ActionResult> result = new List<ActionResult>() { consequencia };
            return result;
        }
    }
}
