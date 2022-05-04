using NoteQuest.Domain.Core;
using NoteQuest.Domain.MasmorraContext.Interfaces;
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

        public string Entrar()
        {
            throw new System.NotImplementedException();
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
    }
}
