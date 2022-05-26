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
using NoteQuest.Domain.MasmorraContext.Factories;
using NoteQuest.Domain.MasmorraContext.DTO;

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
            Mock<IMasmorraRepository> masmorraRepositoryMock = new();
            Mock<ISegmentoBuilder> segmentoBuilderMock = new();
            Mock<IPortaEntrada> portaEntradaMock = new();
            masmorraRepositoryMock.Setup(w => w.PegarNomesMasmorra()).Returns(masmorraNomes);
            IMasmorraRepository masmorraRepository = masmorraRepositoryMock.Object;
            NoteQuest.Domain.MasmorraContext.Entities.Masmorra masmorra = new NoteQuest.Domain.MasmorraContext.Entities.Masmorra(masmorraRepository, segmentoBuilderMock.Object, portaEntradaMock.Object);

            string nomeMasmorra = masmorra.GerarNome(1,1,1);

            string valorEsperado = $"{nomeParte1} {nomeParte2} {nomeParte3}";
            Assert.AreEqual(valorEsperado, nomeMasmorra);
        }
    }
}
