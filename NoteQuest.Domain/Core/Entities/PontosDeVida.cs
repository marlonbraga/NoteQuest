using NoteQuest.Domain.Core.Interfaces;
using System;

namespace NoteQuest.Domain.Core.Entities
{
    public class PontosDeVida : IPontosDeVida
    {
        public int Pv { get; set; }
        public int PvMaximo { get; set; }

        public void Alterar(int Pv)
        {
            throw new NotImplementedException();
        }

        public void AlterarMaximo(int Pv)
        {
            throw new NotImplementedException();
        }

        public void RecuperarTudo()
        {
            throw new NotImplementedException();
        }
    }
}
