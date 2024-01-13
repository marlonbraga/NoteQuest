using NoteQuest.Domain.Core.Interfaces.Inventario;
using System.Collections.Generic;

namespace NoteQuest.Domain.ItensContext.Interfaces
{
    public interface IRepositorio
    {
        public string Titulo { get; set; }

        public IDictionary<int, IItem> Conteudo { get; set; }
        
        public void PegarItem(int indice);

        public void PegarItem(IItem item);
    }
}
