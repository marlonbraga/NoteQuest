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
        //[TestMethod]
        //public void Sala_Entrar_Sucesso()
        //{
        //    IPortaComum porta = new Porta();
        //    BaseSegmento salaAtual = new Sala(porta, "Sala inicial");
        //    BaseSegmento salaAlvo = new Sala(porta, "Sala posterior");

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
