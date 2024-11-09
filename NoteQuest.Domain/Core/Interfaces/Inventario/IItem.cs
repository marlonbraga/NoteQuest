using NoteQuest.Domain.Core.Interfaces.Personagem;

namespace NoteQuest.Domain.Core.Interfaces.Inventario
{
    public enum AcaoItem
    {
        Usar_Equipar = 0,
        Descartar = 1,
        None = 2
    }
    public interface IItem
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
