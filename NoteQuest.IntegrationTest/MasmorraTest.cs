using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteQuest.CLI.IoC;
using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;

namespace NoteQuest.IntegrationTest
{
    [TestClass]
    public class MasmorraTest
    {
        [TestMethod]
        public void Masmorra_EncontrarSalaFinal_Sucesso()
        {
            IContainer Container = new Container();
            //EscolhaFacade EscolhaFacade = new EscolhaFacade(Container);
            IMasmorra masmorra = Masmorra.GerarMasmorra(Container.MasmorraRepository);
            ConsequenciaDTO consequencia = masmorra.EntrarEmMasmorra();

            Assert.AreEqual(4, consequencia.Escolhas.Count);
            Assert.AreEqual(0, consequencia.Segmento.Andar);
            Assert.AreEqual("Descer escadaria", consequencia.Escolhas[0].Acao.Titulo);
            Assert.AreEqual("Verificar porta", consequencia.Escolhas[1].Acao.Titulo);
            Assert.AreEqual("Verificar porta", consequencia.Escolhas[2].Acao.Titulo);
            Assert.AreEqual("Sair de Masmorra", consequencia.Escolhas[3].Acao.Titulo);

            consequencia = consequencia.Escolhas[1].Acao.Executar(6);
            Assert.AreEqual("A porta está aberta", consequencia.Descricao.Trim());
            Assert.AreEqual("Entrar pela porta de direita", consequencia.Escolhas[1].Acao.Titulo);
            Assert.AreEqual("Verificar porta", consequencia.Escolhas[2].Acao.Titulo);

            consequencia = consequencia.Escolhas[2].Acao.Executar(6);
            Assert.AreEqual("A porta está aberta", consequencia.Descricao.Trim());
            Assert.AreEqual("Entrar pela porta de direita", consequencia.Escolhas[1].Acao.Titulo);
            Assert.AreEqual("Entrar pela porta de tras", consequencia.Escolhas[2].Acao.Titulo);

            consequencia = consequencia.Escolhas[1].Acao.Executar(0);
            Assert.IsTrue(consequencia.Descricao.Contains("#1"));
            Assert.IsTrue(consequencia.Descricao.Contains("Pequena sala"));
            Assert.AreEqual(0, consequencia.Segmento.Andar);
            Assert.AreEqual(1, consequencia.Escolhas.Count);

            consequencia = consequencia.Escolhas[0].Acao.Executar();
            Assert.IsTrue(consequencia.Descricao.Contains("#0"));

            consequencia = consequencia.Escolhas[2].Acao.Executar(0);
            Assert.IsTrue(consequencia.Descricao.Contains("#2"));
            Assert.IsTrue(consequencia.Descricao.Contains("Pequena sala"));
            Assert.AreEqual(0, consequencia.Segmento.Andar);
            Assert.AreEqual(1, consequencia.Escolhas.Count);

            consequencia = consequencia.Escolhas[0].Acao.Executar();
            Assert.IsTrue(consequencia.Descricao.Contains("#0"));

            consequencia = consequencia.Escolhas[0].Acao.Executar();
            Assert.IsTrue(consequencia.Descricao.Contains("#3"));
            Assert.IsTrue(consequencia.Descricao.Contains("Corredor"));
        }
    }
}