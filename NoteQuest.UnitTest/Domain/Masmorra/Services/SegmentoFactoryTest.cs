using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.UnitTest.Base;
using Moq;
using System.Threading.Tasks;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.Services;
using NoteQuest.Domain.MasmorraContext.DTO;
using System;

namespace NoteQuest.UnitTest
{
    [TestClass]
    public class SegmentoFactoryTest : BaseTest
    {
        [TestMethod]
        public void SegmentoFactory_Criar_Sucesso()
        {
            Mock<IMasmorra> masmorraMock = new();
            IMasmorra masmorra = masmorraMock.Object;

            MasmorraDataDTO masmorraData = new();
            Mock<IMasmorraRepository> masmorraRepositoryMock = new();
            masmorraRepositoryMock.Setup(w => w.PegarDadosMasmorra(It.IsAny<string>())).Returns(masmorraData);
            IMasmorraRepository masmorraRepository = masmorraRepositoryMock.Object;
            SegmentoFactory segmentoFactory = SegmentoFactory.Instancia(masmorraRepository);

            Assert.IsNotNull(segmentoFactory);
        }

        [TestMethod]
        public void SegmentoFactory_GerarSalaInicial_Sucesso()
        {
            #region Arrange
            Mock<IMasmorra> masmorraMock = new();
            IMasmorra masmorra = masmorraMock.Object;

            Mock<IMasmorraRepository> masmorraRepositoryMock = new();
            Mock<ISegmentoFactory> segmentoFactoryMock = new();

            IMasmorraRepository masmorraRepository = masmorraRepositoryMock.Object;
            ISegmentoFactory segmentoFactory = segmentoFactoryMock.Object;

            int qtdPortas = 3;
            string descricao = "descrição-segmento-inicial";

            BaseSegmento segmentoInicial = new SegmentoInicial(qtdPortas, descricao, masmorraRepository, segmentoFactory, masmorra);

            Mock<IMasmorraData> masmorraDataMock = new();
            masmorraDataMock.Setup(w => w.SegmentoInicial).Returns((SegmentoInicial)segmentoInicial);
            IMasmorraData masmorraData = masmorraDataMock.Object;

            masmorraRepositoryMock.Setup(w => w.PegarDadosMasmorra(It.IsAny<string>())).Returns(masmorraData);
            masmorraRepository = masmorraRepositoryMock.Object;
            SegmentoFactory.Instancia(masmorraRepository);
            #endregion

            (string, BaseSegmento) tupla = SegmentoFactory.Instancia(masmorraRepository).GeraSegmentoInicial(masmorra);

            Assert.AreEqual(segmentoInicial, tupla.Item2);
        }

        [TestMethod]
        public void SegmentoFactory_GeraSegmento_Sucesso()
        {
            int qtdPortas = 3;
            string descricao = "descrição-segmento-inicial";
            Mock<IPortaComum> portaInicialMock = new();
            portaInicialMock.SetupAllProperties();
            portaInicialMock.Setup(w => w.InvertePorta()).Returns(portaInicialMock.Object);
            IPortaComum portaInicial = portaInicialMock.Object;

            BaseSegmento segmentoInicial = new Corredor(portaInicial, descricao, qtdPortas);

            Mock<ISegmento> segmentoAtualMock = new();
            ISegmento segmentoAtual = segmentoAtualMock.Object;

            portaInicialMock = new();
            portaInicialMock.SetupAllProperties();
            portaInicialMock.Setup(w => w.InvertePorta()).Returns(portaInicialMock.Object);
            portaInicialMock.Setup(w => w.SegmentoAtual).Returns(segmentoInicial);
            portaInicial = portaInicialMock.Object;

            MasmorraDataDTO masmorraData = new()
            {
                TabelaSegmentos = new()
                {
                    TabelaAPartirDeCorredor = new TabelaAPartirDe[] {
                        new TabelaAPartirDe()
                        {
                            Indice = 1,
                            Segmento = SegmentoTipo.sala,
                            Descricao = "descricao-sala",
                            QtdPortas = 3
                        }
                    },
                    TabelaAPartirDeSala = new TabelaAPartirDe[] {
                        new TabelaAPartirDe()
                        {
                            Indice = 1,
                            Segmento = SegmentoTipo.escadaria,
                            Descricao = "descricao-escadaria",
                            QtdPortas = 1
                        }
                    },
                    TabelaAPartirDeEscadaria = new TabelaAPartirDe[] {
                        new TabelaAPartirDe()
                        {
                            Indice = 1,
                            Segmento = SegmentoTipo.corredor,
                            Descricao = "descricao-corredor",
                            QtdPortas = 2
                        }
                    }
                },
                TabelaMonstro = new TabelaMonstro[]
                {
                    new TabelaMonstro()
                    {
                        Indice = 1,
                        Qtd = "2",
                        Nome = "Monstrão",
                        Dano = 5,
                        Pvs = 10,
                        Caracteristicas = ""
                    },
                    new TabelaMonstro()
                    {
                        Indice = 1,
                        Qtd = "2",
                        Nome = "Monstrão",
                        Dano = 5,
                        Pvs = 10,
                        Caracteristicas = ""
                    },
                    new TabelaMonstro()
                    {
                        Indice = 1,
                        Qtd = "2",
                        Nome = "Monstrão",
                        Dano = 5,
                        Pvs = 10,
                        Caracteristicas = ""
                    },
                    new TabelaMonstro()
                    {
                        Indice = 1,
                        Qtd = "2",
                        Nome = "Monstrão",
                        Dano = 5,
                        Pvs = 10,
                        Caracteristicas = ""
                    },
                    new TabelaMonstro()
                    {
                        Indice = 1,
                        Qtd = "2",
                        Nome = "Monstrão",
                        Dano = 5,
                        Pvs = 10,
                        Caracteristicas = ""
                    },
                    new TabelaMonstro()
                    {
                        Indice = 1,
                        Qtd = "2",
                        Nome = "Monstrão",
                        Dano = 5,
                        Pvs = 10,
                        Caracteristicas = ""
                    },
                    new TabelaMonstro()
                    {
                        Indice = 1,
                        Qtd = "2",
                        Nome = "Monstrão",
                        Dano = 5,
                        Pvs = 10,
                        Caracteristicas = ""
                    },
                    new TabelaMonstro()
                    {
                        Indice = 1,
                        Qtd = "2",
                        Nome = "Monstrão",
                        Dano = 5,
                        Pvs = 10,
                        Caracteristicas = ""
                    },
                    new TabelaMonstro()
                    {
                        Indice = 1,
                        Qtd = "2",
                        Nome = "Monstrão",
                        Dano = 5,
                        Pvs = 10,
                        Caracteristicas = ""
                    },
                    new TabelaMonstro()
                    {
                        Indice = 1,
                        Qtd = "2",
                        Nome = "Monstrão",
                        Dano = 5,
                        Pvs = 10,
                        Caracteristicas = ""
                    }
                }
            };
            Mock<IMasmorraRepository> masmorraRepositoryMock = new();
            masmorraRepositoryMock.SetupAllProperties();
            masmorraRepositoryMock.Setup(w => w.PegarDadosMasmorra(It.IsAny<string>())).Returns(masmorraData);
            IMasmorraRepository masmorraRepository = masmorraRepositoryMock.Object;
            SegmentoFactory.Instancia(masmorraRepository);

            BaseSegmento segmentoGerado = SegmentoFactory.GeraSegmento(portaInicial, 0);

            Assert.IsNotNull(segmentoGerado);
        }
    }
}
