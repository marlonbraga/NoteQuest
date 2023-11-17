using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using NoteQuest.Domain.MasmorraContext.Services;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class Masmorra : IMasmorra
    {
        public string Descrição { get; set; }
        public int PortasInexploradas { get; set; }
        public bool FoiExplorada { get; set; }
        public bool FoiConquistada { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        private ConsequenciaDTO Consequencia { get; set; }


        public Masmorra(IPortaEntrada portaEntrada, IMasmorraRepository masmorraRepository, int? indice = null)
        {
            PortaEntrada = portaEntrada;

            (string descricao, BaseSegmento segmentoInicial) entradaEmMasmorra;
            entradaEmMasmorra = SegmentoFactory.Instancia(masmorraRepository).GeraSegmentoInicial();

            BaseSegmento segmentoInicial = entradaEmMasmorra.segmentoInicial;
            Consequencia = new()
            {
                Descricao = $"  {entradaEmMasmorra.descricao}\n  {segmentoInicial.Descricao}",
                Segmento = segmentoInicial,
                Escolhas = segmentoInicial.RecuperaTodasAsEscolhas()
            };
        }

        public ConsequenciaDTO EntrarEmMasmorra()
        {
            return Consequencia;
        }

        public static Masmorra GerarMasmorra(IMasmorraRepository masmorraRepository, int? index = null)
        {
            var entradaEmMasmorra = SegmentoFactory.Instancia(masmorraRepository).GeraSegmentoInicial();
            IPortaEntrada portaEntrada = new PortaEntrada()
            {
                SegmentoAtual = entradaEmMasmorra.segmentoInicial
            };
            return new Masmorra(portaEntrada, masmorraRepository)
            {
                Descrição = entradaEmMasmorra.descricao,
                PortasInexploradas = 1,
                FoiExplorada = false,
                FoiConquistada = false,
            };
        }
    }
}
