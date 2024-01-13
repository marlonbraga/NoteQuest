using NoteQuest.Domain.Core.Interfaces.Inventario;

namespace NoteQuest.Domain.ItensContext.Entities
{
    public class Pergaminho : IItem
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
