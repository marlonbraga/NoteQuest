using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.DTO;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.UnitTest.Base;

namespace NoteQuest.UnitTest.Domain.Masmorra.Entities
{
    [TestClass]
    public class MasmorraTest : BaseTest
    {
        [TestInitialize]
        public void Test_Initialize()
        {

        }

        [TestMethod]
        public void Masmorra_GerarNome_Sucesso()
        {
            string nomeParte1 = $"Dummy1";
            string nomeParte2 = $"Dummy2";
            string nomeParte3 = $"Dummy3";
            IMasmorraNomes masmorraNomes = new MasmorraNomesDTO()
            {
                TipoDeMasmorra = new Tipodemasmorra[] { new Tipodemasmorra() { indice = 1, tipo = nomeParte1 } },
                SegundaParte = new Segundaparte[] { new Segundaparte() { indice = 1, nome = nomeParte2 } },
                TerceiraParte = new Terceiraparte[] { new Terceiraparte() { indice = 1, nome = nomeParte3 } }
            };
            Mock<IClasseBasicaRepository> masmorraRepositoryMock = new();
            Mock<ISegmentoBuilder> segmentoBuilderMock = new();
            Mock<IPortaEntrada> portaEntradaMock = new();
            masmorraRepositoryMock.Setup(w => w.PegarNomesMasmorra()).Returns(masmorraNomes);
            IClasseBasicaRepository masmorraRepository = masmorraRepositoryMock.Object;
            NoteQuest.Domain.MasmorraContext.Entities.Masmorra masmorra = new NoteQuest.Domain.MasmorraContext.Entities.Masmorra(masmorraRepository, segmentoBuilderMock.Object, portaEntradaMock.Object);

            string nomeMasmorra = masmorra.GerarNome(1, 1, 1);

            string valorEsperado = $"{nomeParte1} {nomeParte2} {nomeParte3}";
            Assert.AreEqual(valorEsperado, nomeMasmorra);
        }
    }
}
