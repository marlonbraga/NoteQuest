using NoteQuest.Domain.CombateContext.Entities;
using NoteQuest.Domain.Core.Entities;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.Core.Interfaces.Masmorra.Services;
using NoteQuest.Domain.MasmorraContext.DTO;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using System;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Factories
{
    public class SegmentoBuilder : ISegmentoBuilder
    {
        public IMasmorraData MasmorraData { get; set; }
        public IClasseBasicaRepository MasmorraRepository { get; set; }

        public SegmentoBuilder(IClasseBasicaRepository masmorraRepository)
        {
            MasmorraRepository = masmorraRepository;
        }

        #region SEGMENTO
        public void Build(ushort D6 = 1)
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

        public BaseSegmento GeraSegmento(IPortaComum portaDeEntrada, ushort indice)
        {
            BaseSegmento segmentoAtual = portaDeEntrada.SegmentoAtual;
            SegmentoTipo tipoSegmento = TipoSegmento(segmentoAtual, indice);
            BaseSegmento segmento = GerarSegmentoAPartirDeSegmento(portaDeEntrada, tipoSegmento, indice);

            return segmento;
        }

        private SegmentoTipo TipoSegmento(BaseSegmento segmentoAtual, ushort indice)
        {
            return segmentoAtual.GetType().Name switch
            {
                "Sala" => MasmorraData.TabelaSegmentos.TabelaAPartirDeSala[indice].Segmento,
                "Corredor" => MasmorraData.TabelaSegmentos.TabelaAPartirDeCorredor[indice].Segmento,
                "Escadaria" => MasmorraData.TabelaSegmentos.TabelaAPartirDeEscadaria[indice].Segmento,
                _ => MasmorraData.TabelaSegmentos.TabelaAPartirDeSala[indice].Segmento,
            };
        }

        private BaseSegmento GerarSegmentoAPartirDeSegmento(IPortaComum portaDeEntrada, SegmentoTipo tipoSegmento, ushort indice)
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
            return qtd switch
            {
                "1d6" or "1D6" => D6.Rolagem(1),
                "2d6" or "2D6" => D6.Rolagem(2),
                "3d6" or "3D6" => D6.Rolagem(3),
                null => 0,
                _ => Int32.Parse(qtd),
            };
        }
        #endregion

        #region PORTA
        public IPortaComum CriarPortaComum(BaseSegmento segmentoAtual, Direcao direcao, IEscolha escolha, TabelaAPartirDe segmentoAlvo)
        {
            IPortaComum porta = new PortaComum(MasmorraRepository, this);
            BaseSegmento segmento = GerarSegmentoPorTipo(segmentoAlvo.Segmento);
            porta.Build(segmentoAtual, direcao, escolha, segmento);
            segmento.Build(porta, segmentoAlvo.Descricao, segmentoAlvo.QtdPortas);
            return porta;
        }

        private BaseSegmento GerarSegmentoPorTipo(SegmentoTipo segmentoAlvo)
        {
            return segmentoAlvo switch
            {
                SegmentoTipo.sala => new Sala(this),
                SegmentoTipo.corredor => new Corredor(this),
                SegmentoTipo.escadaria => new Escadaria(this),
                _ => new Sala(this),
            };
        }

        public IPortaComum CriarPortaComum(BaseSegmento segmentoAtual, Direcao direcao, IEscolha escolha = null, BaseSegmento segmentoAlvo = null)
        {
            IPortaComum porta = new PortaComum(MasmorraRepository, this);
            porta.Build(segmentoAtual, direcao, escolha, segmentoAlvo);
            return porta;
        }

        public IPortaEntrada CriarPortaDeEntrada(IList<IEscolha> escolhas)
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
