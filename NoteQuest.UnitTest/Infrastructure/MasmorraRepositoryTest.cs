using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteQuest.Domain.MasmorraContext.DTO;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Infrastructure.Data.Masmorra;
using System.IO;

namespace NoteQuest.UnitTest.Infrasctrucuture.Data.Masmorra
{
    [TestClass]
    public class MasmorraRepositoryTest
    {
        [TestMethod]
        public void MasmorraRepository_PegarDadosEmJson_Sucesso()
        {
            string nomeMasmorra = "Palacio";
            IMasmorraRepository masmorraRepository = new MasmorraRepository();

            MasmorraDataDTO dadosMasmorra = (MasmorraDataDTO)masmorraRepository.PegarDadosMasmorra($@"MasmorrasBasicas\{nomeMasmorra}");

            Assert.IsNotNull(dadosMasmorra);
            Assert.IsNotNull(dadosMasmorra.TabelaSegmentos);
        }

        [TestMethod]
        public void MasmorraRepository_PegarNomesEmJson_Sucesso()
        {
            IMasmorraRepository masmorraRepository = new MasmorraRepository();

            MasmorraNomesDTO dadosMasmorra = (MasmorraNomesDTO)masmorraRepository.PegarNomesMasmorra();

            Assert.IsNotNull(dadosMasmorra);
            Assert.IsNotNull(dadosMasmorra.TipoDeMasmorra);
            Assert.IsNotNull(dadosMasmorra.SegundaParte);
            Assert.IsNotNull(dadosMasmorra.TerceiraParte);
        }
    }
}