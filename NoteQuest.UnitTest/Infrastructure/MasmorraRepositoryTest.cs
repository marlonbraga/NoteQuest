using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteQuest.Domain.Core;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Dados;
using NoteQuest.Domain.MasmorraContext.ObjectValue;
using NoteQuest.Domain.MasmorraContext.Services;
using NoteQuest.Infrastructure.Data.Masmorra;
using System.Collections.Generic;
using System.IO;

namespace NoteQuest.UnitTest.Infrasctrucuture.Data.Masmorra
{
    [TestClass]
    public class MasmorraRepositoryTest
    {
        [TestMethod]
        public void MasmorraRepository_PegarEmJson_Sucesso()
        {
            string nomeMasmorra = "Palacio";
            string CurrentDirectory = Directory.GetCurrentDirectory() + $@"\..\..\..\..\docs\configFiles\";
            IMasmorraRepository masmorraRepository = new MasmorraRepository();

            Domain.MasmorraContext.ObjectValue.MasmorraData dadosMasmorra = (Domain.MasmorraContext.ObjectValue.MasmorraData)masmorraRepository.PegarDadosMasmorra(CurrentDirectory+nomeMasmorra);

            Assert.IsNotNull(dadosMasmorra);
            Assert.IsNotNull(dadosMasmorra.TabelaSegmentos);
        }
    }
}