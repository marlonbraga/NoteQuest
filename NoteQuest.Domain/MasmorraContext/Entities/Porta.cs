using NoteQuest.Domain.Core;
using NoteQuest.Domain.MasmorraContext.Interfaces;
using NoteQuest.Domain.MasmorraContext.Services;
using System.Collections.Generic;

namespace NoteQuest.Domain.MasmorraContext.Entities
{
    public enum EstadoDePorta : int
    {
        desconhcido = 0,
        aberta = 1,
        fechada = 2,
        quebrada = 3
    }
    public class Porta : IPorta
    {
        public int IdPorta { get; set; }
        public EstadoDePorta EstadoDePorta { get; set; }
        public Posicao Posicao { get; set; }
        public Segmento SegmentoAlvo { get; set; }
        public Segmento SegmentoAtual { get; set; }
        public SegmentoFactory segmentoFactory { get; }

        public Porta()
        {
            segmentoFactory = new();
        }
        public Segmento Entrar()
        {
            return SegmentoAlvo.Entrar(this);
        }

        public void VerificarFechadura(int valorD6)
        {
            switch (valorD6)
            {
                case 1:
                    EstadoDePorta = EstadoDePorta.aberta;
                    break;
                case 2:
                case 3:
                    EstadoDePorta = EstadoDePorta.fechada;
                    break;
                case 4:
                case 5:
                case 6:
                    EstadoDePorta = EstadoDePorta.aberta;
                    break;
            }
        }

        public void AbrirFechadura()
        {
            EstadoDePorta = EstadoDePorta.aberta;
        }

        public void QuebrarPorta()
        {
            EstadoDePorta = EstadoDePorta.aberta;
        }

        public Segmento ExpiarSala(IPorta portaDeEntrada)
        {
            return segmentoFactory.GeraSegmento(portaDeEntrada, D6.Rolagem(1));
        }
    }
}
