using NoteQuest.Domain.Core.Interfaces.PersonagemContext.Data;
using NoteQuest.Domain.Core.Interfaces.PersonagemContext;
using System.Collections.Generic;
using NoteQuest.Domain.Core.Classes;

namespace NoteQuest.Domain.Core
{
    public class ClasseRepository : IClasseRepository
    {
        public Dictionary<int, IClasse> ClassesBasicas { get; set; }

        public ClasseRepository()
        {
            ClassesBasicas = new Dictionary<int, IClasse>();
            ClassesBasicas.Add(2, new Mendigo());
            //ClassesBasicas.Add(3, new Coveiro());
            //ClassesBasicas.Add(4, new Nobre());
            //ClassesBasicas.Add(5, new Estudante());
            //ClassesBasicas.Add(6, new Ferreiro());
            //ClassesBasicas.Add(7, new Guarda());
            //ClassesBasicas.Add(8, new Cozinheiro());
            //ClassesBasicas.Add(9, new Chaveiro());
            //ClassesBasicas.Add(10, new Lenhador());
            //ClassesBasicas.Add(11, new Minerador());
            //ClassesBasicas.Add(12, new Gladiador());
        }

        public IClasse PegarClasseBasica(int indice)
        {
            return ClassesBasicas.GetValueOrDefault(indice);
        }
    }
}
