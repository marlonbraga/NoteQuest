using NoteQuest.Domain.Core.Interfaces;
using System;

namespace NoteQuest.Domain.Core.Entities
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
    }
}
