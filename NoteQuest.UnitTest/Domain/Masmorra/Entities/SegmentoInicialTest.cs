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
    public class SegmentoInicialTest : BaseTest
    {
        [TestMethod]
        public void SegmentoInicial_Criar_Sucesso()
        {
            Mock<IMasmorra> masmorraMock = new();
            Mock<IMasmorraRepository> masmorraRepositoryMock = new();
            Mock<ISegmentoFactory> segmentoFactoryMock = new();

            IMasmorra masmorra = masmorraMock.Object;
            IMasmorraRepository masmorraRepository = masmorraRepositoryMock.Object;
            ISegmentoFactory segmentoFactory = segmentoFactoryMock.Object;

            int qtdPortas = 3;
            string descricao = "descrição-segmento-inicial";

            BaseSegmento segmentoInicial = new SegmentoInicial(qtdPortas, descricao, masmorraRepository, segmentoFactory, masmorra);

            Assert.AreEqual(descricao, segmentoInicial.Descricao);
            Assert.AreEqual(qtdPortas+1, segmentoInicial.Portas.Count);
            Assert.AreEqual("NoteQuest.Domain.MasmorraContext.Services.Acoes.SairDeMasmorra", segmentoInicial.Portas[3].Escolhas[0].Acao.ToString());
        }
    }
}
