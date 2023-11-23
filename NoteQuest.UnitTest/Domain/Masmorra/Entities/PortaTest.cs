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

namespace NoteQuest.UnitTest
{
    [TestClass]
    public class PortaTest : BaseTest
    {
        [TestMethod]
        public void Porta_Criar_Sucesso()
        {
            Mock<IMasmorraRepository> masmorraRepositoryMock = new();
            IMasmorraRepository masmorraRepository = masmorraRepositoryMock.Object;

            PortaComum portaComum = new PortaComum(masmorraRepository);

            Assert.AreEqual(portaComum.MasmorraRepository, masmorraRepository);
        }

        [TestMethod]
        public void Porta_CriarAPartirDeSegmento_Sucesso()
        {
            Mock<IPortaComum> portaInicialMock = new();
            portaInicialMock.SetupAllProperties();
            portaInicialMock.Setup(w => w.InvertePorta()).Returns(portaInicialMock.Object);
            IPortaComum portaInicial = portaInicialMock.Object;
            Corredor segmento = new(portaInicial, "descrição", 2);
            Posicao posicao = Posicao.frente;
            PortaComum portaComum = new (segmento, posicao);

            Assert.AreEqual(segmento, portaComum.SegmentoAtual);
            Assert.AreEqual(posicao, portaComum.Posicao);
            Assert.AreEqual("NoteQuest.Domain.MasmorraContext.Services.Acoes.VerificarPorta", portaComum.Escolhas[0].Acao.ToString());
        }

        [TestMethod]
        public void Porta_VerificarFechadura_Sucesso()
        {
            Mock<IPortaComum> portaInicialMock = new();
            portaInicialMock.SetupAllProperties();
            portaInicialMock.Setup(w => w.InvertePorta()).Returns(portaInicialMock.Object);
            IPortaComum portaInicial = portaInicialMock.Object;
            Corredor segmento = new(portaInicial, "descrição", 2);
            Posicao posicao = Posicao.frente;
            PortaComum portaComum = new(segmento, posicao);

            EstadoDePorta estado1 = portaComum.VerificarFechadura(1);
            EstadoDePorta estado2 = portaComum.VerificarFechadura(2);
            EstadoDePorta estado3 = portaComum.VerificarFechadura(3);
            EstadoDePorta estado4 = portaComum.VerificarFechadura(4);
            EstadoDePorta estado5 = portaComum.VerificarFechadura(5);
            EstadoDePorta estado6 = portaComum.VerificarFechadura(6);

            Assert.AreEqual(EstadoDePorta.aberta, estado1);
            Assert.AreEqual(EstadoDePorta.fechada, estado2);
            Assert.AreEqual(EstadoDePorta.fechada, estado3);
            Assert.AreEqual(EstadoDePorta.aberta, estado4);
            Assert.AreEqual(EstadoDePorta.aberta, estado5);
            Assert.AreEqual(EstadoDePorta.aberta, estado6);
        }

        [TestMethod]
        public void Porta_InvertePorta_Sucesso()
        {
            Mock<IPortaComum> portaInicialMock = new();
            portaInicialMock.SetupAllProperties();
            portaInicialMock.Setup(w => w.InvertePorta()).Returns(portaInicialMock.Object);
            IPortaComum portaInicial = portaInicialMock.Object;
            Corredor segmento = new(portaInicial, "descrição", 2);
            Posicao posicao = Posicao.frente;
            PortaComum portaComum = new(segmento, posicao);

            IPortaComum portaInversa = portaComum.InvertePorta();

            Assert.AreEqual(portaComum.SegmentoAlvo, portaInversa.SegmentoAtual);
            Assert.AreEqual(portaComum.SegmentoAtual, portaInversa.SegmentoAlvo);

            Assert.AreEqual("NoteQuest.Domain.MasmorraContext.Services.Acoes.EntrarPelaPorta", portaInversa.Escolhas[0].Acao.ToString());

            Assert.AreEqual(Posicao.tras, portaInversa.Posicao);
        }
    }
}
