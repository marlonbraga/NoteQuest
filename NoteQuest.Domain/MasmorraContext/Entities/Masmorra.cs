using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using NoteQuest.Domain.MasmorraContext.Services;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NoteQuest.Domain.Core;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public class Masmorra : IMasmorra
    {
        public string Nome { get; set; }
        public string Descrição { get; set; }
        public IPortaEntrada PortaEntrada { get; set; }
        public IMasmorraRepository MasmorraRepository { get; set; }
        private ConsequenciaDTO Consequencia { get; set; }

        public BaseSegmento SalaFinal { get; set; }
        public int QtdPortasInexploradas { get; set; }
        public bool FoiExplorada { get; set; }
        public bool FoiConquistada { get; set; }
        
        public Masmorra(IPortaEntrada portaEntrada, IMasmorraRepository masmorraRepository/*, int? index = null*/)
        {
            portaEntrada.Masmorra = this;
            PortaEntrada = portaEntrada;
            MasmorraRepository = masmorraRepository;

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
                Descricao = $"  {entradaEmMasmorra.descricao}",
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
            Masmorra masmorra = new (portaEntrada, masmorraRepository/*, index*/)
            {
                Nome = "Masmorra teste",
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

        public string GerarNome(int indice1, int indice2, int indice3)
        {
            IMasmorraNomes masmorraNomes = MasmorraRepository.PegarNomesMasmorra();

            string nomeParte1 = masmorraNomes.TipoDeMasmorra[indice1];
            string nomeParte2 = masmorraNomes.SegundaParte[indice2];
            string nomeParte3 = masmorraNomes.TerceiraParte[indice3];

            nomeParte3 = TratarGenero(nomeParte2, nomeParte3);

            Nome = $"{nomeParte1} {nomeParte2} {nomeParte3}";

            return Nome;
        }

        private string TratarGenero(string nomeParte2, string nomeParte3)
        {
            if (nomeParte2[1] == 'o' || nomeParte2[1] == 'a')
            {
                nomeParte3 = nomeParte3.Replace("o(a)", nomeParte2[1].ToString());
            }
            return nomeParte3;
        }
        public string BuscarTipo(int indice)
        {
            IMasmorraNomes masmorraNomes = MasmorraRepository.PegarNomesMasmorra();
            return masmorraNomes.TipoDeMasmorra[indice];
        }
        public string BuscarSegundaParteDoNome(ushort indice)
        {
            IMasmorraNomes masmorraNomes = MasmorraRepository.PegarNomesMasmorra();
            return masmorraNomes.SegundaParte[indice];
        }
        public string BuscarTerceiraParteDoNome(ushort indice)
        {
            IMasmorraNomes masmorraNomes = MasmorraRepository.PegarNomesMasmorra();
            return masmorraNomes.TerceiraParte[indice];
        }
    }
}
