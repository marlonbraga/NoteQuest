using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra;
using NoteQuest.Domain.Core.Interfaces.Masmorra.Services;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Factories;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.UnitTest.Base;

namespace NoteQuest.UnitTest
{
    [TestClass]
    public class PortaTest : BaseTest
    {
        private Mock<IClasseBasicaRepository> _masmorraRepositoryMock = new();
        private Mock<ISegmentoBuilder> _segmentoFactoryMock = new();
        private IClasseBasicaRepository _masmorraRepository;
        private ISegmentoBuilder _segmentoFactory;

        [TestInitialize]
        public void Test_Initialize()
        {
            _masmorraRepositoryMock = new();
            _segmentoFactoryMock = new();
            _masmorraRepository = _masmorraRepositoryMock.Object;
            _segmentoFactory = new SegmentoBuilder(_masmorraRepository);
        }

        [TestMethod]
        public void Porta_Criar_Sucesso()
        {
            PortaComum porta = new(_masmorraRepository, _segmentoFactory);

            Assert.AreEqual(porta.MasmorraRepository, _masmorraRepository);
        }
        //[TestMethod]
        //public void Porta_CriarAPartirDeSegmento_Sucesso()
        //{
        //    #region Arrange Segmento
        //    Mock<ISegmentoBuilder> portaFactoryMock = new();
        //    ISegmentoBuilder portaFactory = portaFactoryMock.Object;
        //    Mock<IPortaComum> portaInicialMock = new();
        //    portaInicialMock.SetupAllProperties();
        //    portaInicialMock.Setup(w => w.InvertePorta()).Returns(portaInicialMock.Object);
        //    IPortaComum portaInicial = portaInicialMock.Object;
        //    Corredor segmento = new(portaFactory);
        //    segmento.Build(portaInicial, "descrição", 2);
        //    Posicao posicao = Posicao.frente;
        //    #endregion

        //    Mock<IClasseBasicaRepository> masmorraRepositoryMock = new();
        //    IClasseBasicaRepository masmorraRepository = masmorraRepositoryMock.Object;
        //    Mock<IVerificarPortaService> acaoMock = new();
        //    IVerificarPortaService acao = acaoMock.Object;
        //    Mock<ISegmentoBuilder> acaoFactoryMock = new();
        //    acaoFactoryMock.Setup(x => x.CriarVerificarPortaService(It.IsAny<IPortaComum>())).Returns(acao);
        //    ISegmentoBuilder acaoFactory = acaoFactoryMock.Object;

        //    PortaComum porta = new(masmorraRepository, acaoFactory);
        //    porta.Build(segmento, posicao);

        //    Assert.AreEqual(segmento, porta.SegmentoAtual);
        //    Assert.AreEqual(posicao, porta.Posicao);
        //    Assert.AreEqual(acao, porta.Escolhas[0].Acao);
        //}

        //[TestMethod]
        //public void Porta_VerificarFechadura_Sucesso()
        //{
        //    Mock<ISegmentoBuilder> portaFactoryMock = new();
        //    ISegmentoBuilder portaFactory = portaFactoryMock.Object;
        //    Mock<IPortaComum> portaInicialMock = new();
        //    portaInicialMock.SetupAllProperties();
        //    portaInicialMock.Setup(w => w.InvertePorta()).Returns(portaInicialMock.Object);
        //    IPortaComum portaInicial = portaInicialMock.Object;
        //    Corredor segmento = new(portaFactory);
        //    segmento.Build(portaInicial, "descrição", 2);
        //    Posicao posicao = Posicao.frente;

        //    Mock<IClasseBasicaRepository> masmorraRepositoryMock = new();
        //    IClasseBasicaRepository masmorraRepository = masmorraRepositoryMock.Object;
        //    Mock<ISegmentoBuilder> acaoFactoryMock = new();
        //    ISegmentoBuilder acaoFactory = acaoFactoryMock.Object;
        //    PortaComum porta = new(masmorraRepository, acaoFactory);
        //    porta.Build(segmento, posicao);

        //    EstadoDePorta estado1 = porta.VerificarFechadura(1);
        //    EstadoDePorta estado2 = porta.VerificarFechadura(2);
        //    EstadoDePorta estado3 = porta.VerificarFechadura(3);
        //    EstadoDePorta estado4 = porta.VerificarFechadura(4);
        //    EstadoDePorta estado5 = porta.VerificarFechadura(5);
        //    EstadoDePorta estado6 = porta.VerificarFechadura(6);

        //    Assert.AreEqual(EstadoDePorta.aberta, estado1);
        //    Assert.AreEqual(EstadoDePorta.fechada, estado2);
        //    Assert.AreEqual(EstadoDePorta.fechada, estado3);
        //    Assert.AreEqual(EstadoDePorta.aberta, estado4);
        //    Assert.AreEqual(EstadoDePorta.aberta, estado5);
        //    Assert.AreEqual(EstadoDePorta.aberta, estado6);
        //}

        //[TestMethod]
        //public void Porta_InvertePorta_Sucesso()
        //{
        //    #region Arrange Segmento
        //    Mock<ISegmentoBuilder> portaFactoryMock = new();
        //    ISegmentoBuilder portaFactory = portaFactoryMock.Object;
        //    Mock<IPortaComum> portaInicialMock = new();
        //    portaInicialMock.SetupAllProperties();
        //    portaInicialMock.Setup(w => w.InvertePorta()).Returns(portaInicialMock.Object);
        //    IPortaComum portaInicial = portaInicialMock.Object;
        //    Corredor segmento = new(portaFactory);
        //    #endregion

        //    Mock<IClasseBasicaRepository> masmorraRepositoryMock = new();
        //    IClasseBasicaRepository masmorraRepository = masmorraRepositoryMock.Object;
        //    Mock<IVerificarPortaService> acaoMock = new();
        //    IVerificarPortaService acao = acaoMock.Object;
        //    Mock<ISegmentoBuilder> acaoFactoryMock = new();
        //    acaoFactoryMock.Setup(x => x.CriarVerificarPortaService(It.IsAny<IPortaComum>())).Returns(acao);
        //    ISegmentoBuilder acaoFactory = acaoFactoryMock.Object;
        //    PortaComum porta = new(masmorraRepository, acaoFactory);
        //    porta.Build(segmento, Posicao.frente);

        //    IPortaComum portaInversa = porta.InvertePorta();

        //    Assert.AreEqual(porta.SegmentoAlvo, portaInversa.SegmentoAtual);
        //    Assert.AreEqual(porta.SegmentoAtual, portaInversa.SegmentoAlvo);
        //    Assert.AreEqual(acao, portaInversa.Escolhas[0].Acao);
        //    Assert.AreEqual(Posicao.tras, portaInversa.Posicao);
        //}
    }
}
