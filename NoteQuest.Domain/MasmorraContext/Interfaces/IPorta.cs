using NoteQuest.Domain.MasmorraContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteQuest.Domain.MasmorraContext.Interfaces
{
    public enum Posicao : int
    {
        frente = 0,
        direita = 1,
        atras = 2,
        esquerda = 3
    }
    public enum EstadoDePorta : int
    {
        desconhcido = 0,
        aberta = 1,
        fechada = 2,
        quebrada = 3
    }
    public interface IPorta
    {
        public Posicao Posicao { get; set; }
        public BaseSegmento SegmentoAtual { get; set; }
    }
}
