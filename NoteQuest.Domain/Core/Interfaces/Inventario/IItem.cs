using NoteQuest.Domain.Core.Interfaces.Personagem;

namespace NoteQuest.Domain.Core.Interfaces.Inventario
{
    public enum AcaoItem
    {
        None = 0,
        Usar = 1,
        Equipar = 2,
        Descartar = 4,
    }
    public interface IItem
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
