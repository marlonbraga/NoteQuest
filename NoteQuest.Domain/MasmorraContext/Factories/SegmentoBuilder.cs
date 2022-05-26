using NoteQuest.Domain.CombateContext.Entities;
using NoteQuest.Domain.Core;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.DTO;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using System;
using System.Collections.Generic;
using NoteQuest.Domain.MasmorraContext.Interfaces.Services;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;

namespace NoteQuest.Domain.MasmorraContext.Factories
{
    public class SegmentoBuilder : ISegmentoBuilder
    {
        public IMasmorraData MasmorraData { get; set; }
        public IMasmorraRepository MasmorraRepository { get; set; }

        public SegmentoBuilder(IMasmorraRepository masmorraRepository)
        {
            MasmorraRepository = masmorraRepository;
        }
        
        #region SEGMENTO
        public void Build(int D6 = 1)
        {
            //TODO: Variar string por parametro int
            MasmorraData = MasmorraRepository.PegarDadosMasmorra("Palacio");
        }

        public Tuple<string, BaseSegmento> GeraSegmentoInicial()
        {
            SegmentoInicial segmento = MasmorraData.SegmentoInicial;
            segmento.Build(this);

            var entradaEmMasmorra = Tuple.Create(MasmorraData.Descricao, (BaseSegmento)segmento);
            return entradaEmMasmorra;
        }

        public BaseSegmento GeraSegmento(IPortaComum portaDeEntrada, int indice)
        {
            BaseSegmento segmentoAtual = portaDeEntrada.SegmentoAtual;
            SegmentoTipo tipoSegmento = TipoSegmento(segmentoAtual, indice);
            BaseSegmento segmento = GerarSegmentoPorSegmento(portaDeEntrada, tipoSegmento, indice);

            return segmento;
        }

        private SegmentoTipo TipoSegmento(BaseSegmento segmentoAtual, int indice)
        {
            switch (segmentoAtual.GetType().Name)
            {
                case "Sala":
                    return MasmorraData.TabelaSegmentos.TabelaAPartirDeSala[indice].Segmento;
                case "Corredor":
                    return MasmorraData.TabelaSegmentos.TabelaAPartirDeCorredor[indice].Segmento;
                case "Escadaria":
                    return MasmorraData.TabelaSegmentos.TabelaAPartirDeEscadaria[indice].Segmento;
                default:
                    return MasmorraData.TabelaSegmentos.TabelaAPartirDeSala[indice].Segmento;
            }
        }

        private BaseSegmento GerarSegmentoPorSegmento(IPortaComum portaDeEntrada, SegmentoTipo tipoSegmento, int indice)
        {
            BaseSegmento segmento = null;
            string descricao = null;
            int qtdPortas = 0;
            switch (tipoSegmento.ToString())
            {
                case "sala":
                    descricao = MasmorraData.TabelaSegmentos.TabelaAPartirDeSala[indice].Descricao;
                    qtdPortas = MasmorraData.TabelaSegmentos.TabelaAPartirDeSala[indice].QtdPortas;
                    segmento = new Sala(this);
                    segmento.Build(portaDeEntrada, descricao, qtdPortas);
                    //TODO: Não adicionar Monstros nem Conteúdo a sala recém criada
                    segmento = ((Sala)segmento).AdicionaMonstros(GeraMonstros((TabelaMonstro)MasmorraData.TabelaMonstro[D6.Rolagem(2)]));
                    break;
                case "corredor":
                    descricao = MasmorraData.TabelaSegmentos.TabelaAPartirDeCorredor[indice].Descricao;
                    qtdPortas = MasmorraData.TabelaSegmentos.TabelaAPartirDeCorredor[indice].QtdPortas;
                    segmento = new Corredor(this);
                    segmento.Build(portaDeEntrada, descricao, qtdPortas);
                    break;
                case "escadaria":
                    descricao = MasmorraData.TabelaSegmentos.TabelaAPartirDeEscadaria[indice].Descricao;
                    qtdPortas = MasmorraData.TabelaSegmentos.TabelaAPartirDeEscadaria[indice].QtdPortas;
                    segmento = new Escadaria(this);
                    segmento.Build(portaDeEntrada, descricao, qtdPortas);
                    break;
            }
            return segmento;
        }
        //TODO: Fere Single Responsability Principle (SRP)
        //  Levar método privado para classe responsável
        private List<Monstro> GeraMonstros(TabelaMonstro tabelaMonstro)
        {
            Monstro monstro = new(tabelaMonstro.Nome, tabelaMonstro.Dano, tabelaMonstro.Pvs)
            {
                Caracteristicas = new string[] { tabelaMonstro.Caracteristicas }
            };
            List<Monstro> monstros = new();
            for (int i = 0; i < ConverteQtdMonstros(tabelaMonstro.Qtd); i++)
            {
                monstros.Add(monstro);
            }

            return monstros;
        }
        //TODO: Fere Single Responsability Principle (SRP)
        //  Levar método privado para classe responsável
        private int ConverteQtdMonstros(string qtd)
        {
            switch (qtd)
            {
                case "1d6":
                case "1D6":
                    return D6.Rolagem(1);
                case "2d6":
                case "2D6":
                    return D6.Rolagem(2);
                case "3d6":
                case "3D6":
                    return D6.Rolagem(3);
                case null:
                    return 0;
                default:
                    return Int32.Parse(qtd);
            }
        }
        #endregion

        #region PORTA
        public IPortaComum CriarPortaComum(BaseSegmento segmentoAtual, Posicao posicao)
        {
            IPortaComum porta = new PortaComum(MasmorraRepository, this);
            porta.Build(segmentoAtual, posicao);
            return porta;
        }

        public IPortaEntrada CriarPortaDeEntrada(List<IEscolha> escolhas)
        {
            PortaEntrada porta = new()
            {
                Escolhas = escolhas
            };
            return porta;
        }
        #endregion

        #region AÇÃO
        public IEntrarPelaPortaService CriarEntrarPelaPortaService(IPortaComum portaComum)
        {
            IEntrarPelaPortaService service = new EntrarPelaPortaService(portaComum, this);

            return service;
        }

        public IQuebrarPortaService CriarQuebrarPortaService(IPortaComum portaComum)
        {
            return new QuebrarPortaService(portaComum);
        }

        public IAbrirFechaduraService CriarAbrirFechaduraService(IPortaComum portaComum)
        {
            return new AbrirFechaduraService(portaComum);
        }

        public IVerificarPortaService CriarVerificarPortaService(IPortaComum portaComum)
        {
            return new VerificarPortaService(portaComum);
        }
        #endregion
    }
}
