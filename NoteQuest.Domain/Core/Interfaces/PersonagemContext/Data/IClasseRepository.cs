using System.Collections.Generic;

namespace NoteQuest.Domain.Core.Interfaces.PersonagemContext.Data
{
    public interface IClasseRepository
    {
        public Dictionary<int, IClasse> ClassesBasicas { get; set; }

        public IClasse PegarClasseBasica(int indice);
    }
}
