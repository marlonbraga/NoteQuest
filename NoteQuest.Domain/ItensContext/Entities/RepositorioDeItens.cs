using NoteQuest.Domain.Core.Interfaces.Inventario;
using NoteQuest.Domain.ItensContext.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace NoteQuest.Domain.ItensContext.Entities
{
    public class RepositorioDeItens : IRepositorio
    {
        public string Titulo { get; set; }

        public IDictionary<int, IItem> Conteudo { get; set; }

        public void PegarItem(int indice)
        {
            Conteudo.Remove(indice);
        }

        public void PegarItem(IItem item)
        {
            int indice = Conteudo.FirstOrDefault(x => x.Value == item).Key;
            Conteudo.Remove(indice);
        }
    }
}
