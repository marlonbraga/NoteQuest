using System.Collections.Generic;

namespace NoteQuest.Domain.Core.Interfaces.Personagem.Data
{
    public interface IMagiaRepository
    {
        public Dictionary<int, IMagia> MagiasBasicas { get; set; }

        public IMagia PegarMagiaBasica(int indice);
    }
}
