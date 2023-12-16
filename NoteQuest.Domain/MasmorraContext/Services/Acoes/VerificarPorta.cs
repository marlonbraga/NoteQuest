using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Services.Factories;
using System;
using System.Collections.Generic;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.Core.ObjectValue;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class VerificarPorta : IEvent
    {
        public IPortaComum Porta { get; set; }
        public IMasmorra Masmorra { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int? IndicePreDefinido { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public string EventTrigger { get; set; }
        public Func<IEnumerable<ActionResult>> Efeito { get; set; }
        public IPersonagem Personagem {get; set; }
        public IDictionary<string, IEvent> ChainedEvents { get; set; }

        public VerificarPorta(IPortaComum porta, int? indicePreDefinido)
        {
            EventTrigger = nameof(VerificarPorta);
            Efeito = delegate { return Executar(); };
            Porta = porta;
            Masmorra = porta.Masmorra;
            Titulo = "Verificar porta";
            Descricao = "Pode acionar armadilhas";
            IndicePreDefinido = indicePreDefinido;
            ChainedEvents = new Dictionary<string, IEvent>();
            ChainedEvents["Armadilha"] = Porta.Masmorra?.ArmadilhaFactory.GeraArmadilha(Porta.Masmorra, 1);
        }

        public IEnumerable<ActionResult> Executar(int? indicePorta = null, int? indiceArmadilha = null)
        {
            List<ActionResult> result = new(2);

            EstadoDePorta estado = Porta.VerificarFechadura(indicePorta ?? D6.Rolagem());
            if (estado == EstadoDePorta.aberta)
                Porta.SegmentoAlvo ??= Porta.SegmentoAtual.Masmorra.SegmentoFactory.GeraSegmento(Porta, IndicePreDefinido ?? indicePorta ?? D6.Rolagem(deslocamento: true));

            indiceArmadilha ??= D6.Rolagem();
            if (indiceArmadilha == 1)
            {
                ActionResult eventoArmadilha = new ("ARMADILHA!");
                result.Add(eventoArmadilha);
                result.AddRange(ChainedEvents["Armadilha"].Efeito.Invoke());
            }

            BaseSegmento segmentoAtual = Porta.SegmentoAtual;
            List<IEscolha> escolhas = segmentoAtual.RecuperaTodasAsEscolhas();
            string descicao = $"\n  A porta está {estado}";
            ActionResult dungeonConsequence = new DungeonConsequence(descicao, segmentoAtual);
            result.Add(dungeonConsequence);

            return result;
        }
    }
}
