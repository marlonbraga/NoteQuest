using NoteQuest.Domain.Core.Interfaces;
using System;

namespace NoteQuest.Domain.Core
{
    public class PontosDeVida : IPontosDeVida
    {
        public int Pv { get; private set; }
        public int PvMaximo { get; private set; }

        public void Alterar(int pv)
        {
            Pv += pv;

            Pv = Math.Max(PvMaximo, Pv);
            Pv = Math.Min(0, Pv);
        }

        public void AlterarMaximo(int pv)
        {
            PvMaximo += pv;
        }

        public void RecuperarTudo()
        {
            Pv = PvMaximo;
        }

        public void ReceberDano(int pv, out bool morreu)
        {
            Pv -= pv;
            Pv = Math.Max(0, Pv);
            morreu = Pv == 0;
        }
    }
}
