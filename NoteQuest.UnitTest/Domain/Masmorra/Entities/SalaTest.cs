using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.UnitTest.Base;
using Moq;
using System.Threading.Tasks;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Services.Acoes;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.CombateContext.Entities;
using System.Collections.Generic;

namespace NoteQuest.UnitTest
{
    [TestClass]
    public class SalaTest : BaseTest
    {
        [TestMethod]
        public void Sala_Criar_InverterPortaComSucesso()
        {
            Mock<IPortaComum> portaInicial = new();
            portaInicial.SetupAllProperties();
            portaInicial.Setup(w => w.InvertePorta()).Returns(portaInicial.Object);

            Mock<IPortaComum> portaDeSaida = new();
            Mock<IPortaComum> portaDeEntrada = new();
            portaDeSaida.SetupAllProperties();
            portaDeSaida.Setup(w => w.InvertePorta()).Returns(portaDeEntrada.Object);
            portaDeEntrada.SetupAllProperties();
            portaDeEntrada.Setup(w => w.InvertePorta()).Returns(portaDeSaida.Object);

            BaseSegmento salaAtual = new Sala(portaInicial.Object, "Sala inicial", 0);
            BaseSegmento salaAlvo = new Sala(portaDeEntrada.Object, "Sala posterior", 3);

            Assert.AreEqual(salaAtual.Descricao, "Sala inicial");
            Assert.AreEqual(salaAtual.Portas.Count, 1);

            Assert.AreEqual(salaAlvo.Descricao, "Sala posterior");
            Assert.AreEqual(salaAlvo.Portas.Count, 4);
            Assert.AreEqual(salaAlvo.Portas[0], portaDeSaida.Object);
        }

        [TestMethod]
        public void Sala_AdicionarMonstros_Sucesso()
        {
            Mock<IPortaComum> portaInicial = new();
            portaInicial.SetupAllProperties();
            portaInicial.Setup(w => w.InvertePorta()).Returns(portaInicial.Object);
            Monstro monstro = new("NOME_DE_MONSTRO", 5, 10);
            List<Monstro> monstros = new() { monstro };
            Sala sala = new (portaInicial.Object, "Sala", 0);
            sala = sala.AdicionaMonstros(monstros);

            Assert.AreEqual(sala.Monstros, monstros);
        }

        [TestMethod]
        public void Moq_Test()
        {
            /*
                [TestClass]
                public class DataImportReportActionTest : BaseMockTest
                {
                    [TestMethod]
                    public async Task Dootax_DataImportReportAction_Success()
                    {
                        //Arrange
                        DataImportReportDTO outputObject = new DataImportReportDTO() { Content = new DataImportDTO[1] { new DataImportDTO() { Records = new RecordsDTO() } } };
                        OutputStatus expectedOutput = new OutputStatus() { Success = true };
                        BaseResponseDTO response = new BaseResponseDTO(expectedOutput, outputObject);
                        List<BaseResponseDTO> expectedResponse = new() { response };
                        ImportedFileDTO importedFile = new()
                        {
                            Filename = "teste.txt",
                            Content = "aGVsbG8="
                        };
                        List<ImportedFileDTO> importedFiles = new() { importedFile };

                        Mock<IPagamentoDeTributoService> dootaxService = new Mock<IPagamentoDeTributoService>();
                        dootaxService.Setup(w => w.DataImportReportAsync(It.IsAny<List<ImportedFileDTO>>())).ReturnsAsync(expectedResponse);
                        __mockParameters__.Setup(x => x.GetRawInput()).Returns(JsonConvert.SerializeObject(importedFiles));
                        __parameters__ = __mockParameters__.Object;
                        __container__.Register<IPagamentoDeTributoService>(() => dootaxService.Object);

                        //Act
                        ActionResult actionResult = await new DataImportReportAction(__container__).ExecuteReturnableAsync(__ct__, __parameters__, __jobLogger__);
                        List<BaseResponseDTO> result = JsonConvert.DeserializeObject<List<BaseResponseDTO>>(actionResult.GetActionResult().ToString());

                        //Assert
                        Assert.AreEqual(true, result.First().Info.Success);
                    }

                    [TestMethod]
                    public async Task Dootax_DataImportReportAction_Null()
                    {
                        //Arrange
                        OutputStatus expectedOutput = new OutputStatus() { Success = true };
                        BaseResponseDTO response = new BaseResponseDTO(expectedOutput);
                        List<BaseResponseDTO> expectedResponse = new() { response };
                        ImportedFileDTO importedFile = new()
                        {
                            Filename = "teste.txt",
                            Content = "aGVsbG8="
                        };
                        List<ImportedFileDTO> importedFiles = new() { importedFile };

                        Mock<IPagamentoDeTributoService> dootaxService = new Mock<IPagamentoDeTributoService>();
                        dootaxService.Setup(w => w.DataImportReportAsync(importedFiles)).ReturnsAsync(expectedResponse);
                        __mockParameters__.Setup(x => x.GetRawInput()).Returns(JsonConvert.SerializeObject(importedFiles));
                        __mockParameters__.Setup(x => x.GetConnectorParameters<ConnectorConfiguration>()).Returns(NullParameters());
                        __parameters__ = __mockParameters__.Object;
                        __container__.Register<IPagamentoDeTributoService>(() => dootaxService.Object);

                        //Act
                        ActionResult actionResult = await new DataImportReportAction(__container__).ExecuteReturnableAsync(__ct__, __parameters__, __jobLogger__);

                        //Assert
                        Assert.IsNotNull(actionResult);
                        Assert.AreEqual("[]", actionResult.GetActionResult().ToString());
                    }

                    [TestMethod]
                    public async Task Dootax_DataImportReportAction_Empty()
                    {
                        //Arrange
                        OutputStatus expectedOutput = new OutputStatus() { Success = true };
                        BaseResponseDTO response = new BaseResponseDTO(expectedOutput);
                        List<BaseResponseDTO> expectedResponse = new() { response };
                        ImportedFileDTO importedFile = new()
                        {
                            Filename = "teste.txt",
                            Content = "aGVsbG8="
                        };
                        List<ImportedFileDTO> importedFiles = new() { importedFile };

                        Mock<IPagamentoDeTributoService> dootaxService = new Mock<IPagamentoDeTributoService>();
                        dootaxService.Setup(w => w.DataImportReportAsync(importedFiles)).ReturnsAsync(expectedResponse);
                        __mockParameters__.Setup(x => x.GetRawInput()).Returns(string.Empty);
                        __parameters__ = __mockParameters__.Object;
                        __container__.Register<IPagamentoDeTributoService>(() => dootaxService.Object);

                        //Act
                        ActionResult actionResult = await new DataImportReportAction(__container__).ExecuteReturnableAsync(__ct__, __parameters__, __jobLogger__);

                        //Assert
                        Assert.IsNotNull(actionResult);
                        Assert.AreEqual("[]", actionResult.GetActionResult().ToString());
                    }

                    [TestMethod]
                    public void Dootax_DataImportReportAction_Error()
                    {
                        //Arrange
                        ImportedFileDTO importedFile = new()
                        {
                            Filename = "teste.txt",
                            Content = "aGVsbG8="
                        };
                        List<ImportedFileDTO> importedFiles = new() { importedFile };

                        Mock<IPagamentoDeTributoService> dootaxService = new Mock<IPagamentoDeTributoService>();
                        dootaxService.Setup(w => w.DataImportReportAsync(It.IsAny<List<ImportedFileDTO>>())).ThrowsAsync(new Exception());
                        __mockParameters__.Setup(x => x.GetRawInput()).Returns(JsonConvert.SerializeObject(importedFiles));
                        __parameters__ = __mockParameters__.Object;
                        __container__.Register<IPagamentoDeTributoService>(() => dootaxService.Object);

                        //Act & Assert
                        Assert.ThrowsExceptionAsync<Exception>(() => new DataImportReportAction(__container__).ExecuteReturnableAsync(__ct__, __parameters__, __jobLogger__));
                    }
                }
            */
}
    }
}
