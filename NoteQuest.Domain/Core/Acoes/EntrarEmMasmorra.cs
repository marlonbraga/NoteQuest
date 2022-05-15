using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Services;
using System;

namespace NoteQuest.Domain.Core.Acoes
{
    public class EntrarEmMasmorra : IAcao
    {
        public int Indice { get; set; }
        public IMasmorra Masmorra { get; set; }
        public IMasmorraRepository MasmorraRepository { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        public ISegmentoFactory SegmentoFactory { get; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public EntrarEmMasmorra(int indice, IMasmorraRepository masmorraRepository, ISegmentoFactory segmentoFactory, IPortaEntrada portaEntrada)
        {
            Indice = indice;
            MasmorraRepository = masmorraRepository;
            SegmentoFactory = segmentoFactory;
            PortaEntrada = portaEntrada;
            Titulo = "Entrar em masmorra";
            Descricao = "Ambiente escuro e perigoso. Gasta 1 tocha";
        }

        public ConsequenciaDTO Executar()
        {
            Tuple<string, BaseSegmento> entradaEmMasmorra;
            entradaEmMasmorra = ((SegmentoFactory)SegmentoFactory).GeraSegmentoInicial();
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
