using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Personagem;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Services.Factories;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class EntrarEmMasmorra : IEvent
    {
        public int Indice { get; set; }
        public IMasmorra Masmorra { get; set; }
        public IMasmorraRepository MasmorraRepository { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public string EventTrigger { get; set; }
        public IDictionary<string, IEvent> ChainedEvents { get; set; }
        public IPersonagem Personagem { get; set; }
        public Func<IEnumerable<ActionResult>> Efeito { get; set; }

        public EntrarEmMasmorra(int indice, IMasmorraRepository masmorraRepository, IMasmorra masmorra, IPortaEntrada portaEntrada)
        {
            EventTrigger = nameof(EntrarEmMasmorra);
            Efeito = delegate { return Executar(); };
            Indice = indice;
            MasmorraRepository = masmorraRepository;
            Masmorra = masmorra;
            PortaEntrada = portaEntrada;
            Titulo = "Entrar em masmorra";
            Descricao = "Ambiente escuro e perigoso. Gasta 1 tocha";
        }

        public IEnumerable<ActionResult> Executar(int? indice = null)
        {
            (string, BaseSegmento) entradaEmMasmorra;
            entradaEmMasmorra = Masmorra.SegmentoFactory.GeraSegmentoInicial(Masmorra);
            BaseSegmento segmentoInicial = entradaEmMasmorra.Item2;
            string descricao = $"  {entradaEmMasmorra.Item1}\n  {segmentoInicial.Descricao}";
            DungeonConsequence consequencia = new(descricao, segmentoInicial);
            IEnumerable<ActionResult> result = new List<ActionResult>() { consequencia };
            return result;
        }
    }
}
