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

            Porta porta = new Porta(masmorraRepository);

            Assert.AreEqual(porta.MasmorraRepository, masmorraRepository);
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
            Porta porta = new (segmento, posicao);

            Assert.AreEqual(segmento, porta.SegmentoAtual);
            Assert.AreEqual(posicao, porta.Posicao);
            Assert.AreEqual("NoteQuest.Domain.MasmorraContext.Services.Acoes.VerificarPorta", porta.Escolhas[0].Acao.ToString());
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
            Porta porta = new(segmento, posicao);

            EstadoDePorta estado1 = porta.VerificarFechadura(1);
            EstadoDePorta estado2 = porta.VerificarFechadura(2);
            EstadoDePorta estado3 = porta.VerificarFechadura(3);
            EstadoDePorta estado4 = porta.VerificarFechadura(4);
            EstadoDePorta estado5 = porta.VerificarFechadura(5);
            EstadoDePorta estado6 = porta.VerificarFechadura(6);

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
            Porta porta = new(segmento, posicao);

            IPortaComum portaInversa = porta.InvertePorta();

            Assert.AreEqual(porta.SegmentoAlvo, portaInversa.SegmentoAtual);
            Assert.AreEqual(porta.SegmentoAtual, portaInversa.SegmentoAlvo);

            Assert.AreEqual("NoteQuest.Domain.MasmorraContext.Services.Acoes.EntrarPelaPorta", portaInversa.Escolhas[0].Acao.ToString());

            Assert.AreEqual(Posicao.tras, portaInversa.Posicao);
        }
    }
}
