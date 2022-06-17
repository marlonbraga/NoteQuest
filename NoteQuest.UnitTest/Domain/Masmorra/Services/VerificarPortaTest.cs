using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Factories;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;

namespace NoteQuest.UnitTest
{
    [TestClass]
    public class VerificarPortaTest
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

        //[TestMethod]
        //public void Sala_Entrar_Sucesso()
        //{
        //    BaseSegmento salaAtual = new Sala(_segmentoFactory);
        //    BaseSegmento salaAlvo = new Sala(_segmentoFactory);
        //    IPortaComum porta = _segmentoFactory.CriarPortaComum(salaAtual, Posicao.frente);
        //    salaAtual.Build(porta, "Sala inicial", 0);
        //    salaAtual.Build(porta, "Sala posterior", 0);

        //    porta.SegmentoAtual = salaAtual;
        //    porta.SegmentoAlvo = salaAlvo;
        //    salaAtual.Portas.Add(porta);

        //    BaseSegmento novaSala = salaAtual.Entrar(porta);
        //    BaseSegmento segmentoAlvo = ((IPortaComum)salaAtual.Portas[0]).SegmentoAlvo;
        //    BaseSegmento SegmentoAtual = salaAtual.Portas[0].SegmentoAtual;

        //    Assert.AreEqual(salaAlvo.Descricao, novaSala.Descricao);
        //    Assert.AreEqual(segmentoAlvo, SegmentoAtual);
        //    Assert.AreEqual(SegmentoAtual, segmentoAlvo);
        //}
    }
}
