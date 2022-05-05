using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteQuest.Domain.Core;
using NoteQuest.Domain.MasmorraContext.Entities;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Services;
using System.Collections.Generic;

namespace NoteQuest.UnitTest
{
    [TestClass]
    public class SalaTest
    {
        [TestMethod]
        public void Sala_Entrar_Sucesso()
        {
            IPorta porta = new Porta();
            Segmento salaAtual = new Sala(porta, "Sala inicial");
            Segmento salaAlvo = new Sala(porta, "Sala posterior");

            porta.SegmentoAtual = salaAtual;
            porta.SegmentoAlvo = salaAlvo;
            salaAtual.Portas.Add(porta);

            Segmento novaSala = salaAtual.Entrar(porta);

            Assert.AreEqual(salaAlvo.Descricao, novaSala.Descricao);
            Assert.AreEqual(salaAtual.Portas[0].SegmentoAlvo, novaSala.Portas[0].SegmentoAtual);
            Assert.AreEqual(salaAtual.Portas[0].SegmentoAtual, novaSala.Portas[0].SegmentoAlvo);
        }
    }
}
