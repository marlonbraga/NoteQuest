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
using NoteQuest.Domain.MasmorraContext.Interfaces.Services;
using System.Collections.Generic;
using NoteQuest.Domain.MasmorraContext.Factories;

namespace NoteQuest.UnitTest
{
    [TestClass]
    public class SegmentoInicialTest : BaseTest
    {
        private Mock<IMasmorraRepository> _masmorraRepositoryMock = new();
        private Mock<ISegmentoBuilder> _segmentoFactoryMock = new();
        private IMasmorraRepository _masmorraRepository;
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
        public void SegmentoInicial_Criar_Sucesso()
        {
            Mock<ISairDeMasmorraService> acaoMock = new();
            ISairDeMasmorraService acao = acaoMock.Object;
            Mock<IPortaEntrada> portaEntradaMock = new();
            portaEntradaMock.SetupAllProperties();
            IPortaEntrada portaEntrada = portaEntradaMock.Object;

            Mock<ISegmentoBuilder> acaoFactoryMock = new();
            Mock<ISegmentoBuilder> portaFactoryMock = new();
            portaFactoryMock.Setup(x => x.CriarPortaDeEntrada(It.IsAny<List<IEscolha>>())).Returns(portaEntrada);
            Mock<IMasmorraRepository> masmorraRepositoryMock = new();
            Mock<ISegmentoBuilder> segmentoFactoryMock = new();
            IMasmorraRepository masmorraRepository = masmorraRepositoryMock.Object;
            ISegmentoBuilder segmentoFactory = segmentoFactoryMock.Object;
            ISegmentoBuilder portaFactory = portaFactoryMock.Object;
            int qtdPortas = 3;
            string descricao = "descrição-segmento-inicial";

            BaseSegmento segmentoInicial = new SegmentoInicial(descricao, qtdPortas, segmentoFactory);
            segmentoInicial.Build(descricao, qtdPortas);

            Assert.AreEqual(descricao, segmentoInicial.Descricao);
            Assert.AreEqual(qtdPortas+1, segmentoInicial.Portas.Count);
            Assert.AreEqual(portaEntrada, segmentoInicial.Portas[0]);
        }
    }
}
