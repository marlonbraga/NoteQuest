using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Entities;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Interfaces
{
    public enum Posicao : int
    {
        frente = 0,
        esquerda = 1,
        direita = 2,
        tras = 3,
    }
    public enum EstadoDePorta : int
    {
        inverificada = 0,
        aberta = 1,
        fechada = 2,
        quebrada = 3
    }
    public interface IPorta
    {
        public Posicao Posicao { get; set; }
        public BaseSegmento SegmentoAtual { get; set; }
        public List<IEscolha> Escolhas { get; set; }
        EstadoDePorta EstadoDePorta { get; set; }
        public int Andar { get; set; }
        List<IEscolha> AbrirPorta();
        IMasmorra Masmorra { get; set; }
    }
}
