using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using NoteQuest.Domain.MasmorraContext.Services;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class Masmorra : IMasmorra
    {
        public string Nome { get; set; }
        public string Descrição { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        private ConsequenciaDTO Consequencia { get; set; }

        public BaseSegmento SalaFinal { get; set; }
        public int QtdPortasInexploradas { get; set; }
        public bool FoiExplorada { get; set; }
        public bool FoiConquistada { get; set; }
        
        public Masmorra(IPortaEntrada portaEntrada, IMasmorraRepository masmorraRepository, int? indice = null)
        {
            portaEntrada.Masmorra = this;
            PortaEntrada = portaEntrada;

            (string descricao, BaseSegmento segmentoInicial) entradaEmMasmorra;
            entradaEmMasmorra = SegmentoFactory.Instancia(masmorraRepository).GeraSegmentoInicial(this);

            BaseSegmento segmentoInicial = entradaEmMasmorra.segmentoInicial;
            segmentoInicial.Masmorra = this;
            foreach (var porta in segmentoInicial.Portas)
            {
                porta.Masmorra = this;
            }
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
            IPortaEntrada portaEntrada = new PortaEntrada();
            Masmorra masmorra = new (portaEntrada, masmorraRepository)
            {
                Nome = "Palácio Teste",
                QtdPortasInexploradas = 1,
                FoiExplorada = false,
                FoiConquistada = false,
            };
            var entradaEmMasmorra = SegmentoFactory.Instancia(masmorraRepository).GeraSegmentoInicial(masmorra);
            portaEntrada.SegmentoAtual = entradaEmMasmorra.segmentoInicial;
            masmorra.Descrição = entradaEmMasmorra.descricao;
            portaEntrada.Masmorra = masmorra;
            return masmorra;
        }
    }
}
