using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Services;
using NoteQuest.Domain.MasmorraContext.Interfaces.Services;
using System;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class EntrarEmMasmorraService : IEntrarEmMasmorraService
    {
        public int Indice { get; set; }
        public IMasmorra Masmorra { get; set; }
        public IMasmorraRepository MasmorraRepository { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        public ISegmentoBuilder SegmentoBuilder { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public EntrarEmMasmorraService(IMasmorraRepository masmorraRepository, IPortaEntrada portaEntrada, ISegmentoBuilder segmentoBuilder)
        {
            MasmorraRepository = masmorraRepository;
            PortaEntrada = portaEntrada;
            SegmentoBuilder = segmentoBuilder;
            Titulo = "Entrar em masmorra";
            Descricao = "Ambiente escuro e perigoso. Gasta 1 tocha";
        }

        public ConsequenciaDTO Executar()
        {
            Tuple<string, BaseSegmento> entradaEmMasmorra;
            SegmentoBuilder.Build(1);
            entradaEmMasmorra = SegmentoBuilder.GeraSegmentoInicial();
            BaseSegmento segmentoInicial = entradaEmMasmorra.Item2;
            ConsequenciaDTO consequencia = new()
            {
                Descricao = $"  {entradaEmMasmorra.Item1}\n  {segmentoInicial.Descricao}",
                Segmento = segmentoInicial,
                Escolhas = segmentoInicial.RecuperaTodasAsEscolhas()
            };

            return consequencia;
        }
    }
}
