using NoteQuest.Domain.Core.Interfaces.Personagem;

namespace NoteQuest.Domain.Core.Interfaces.Inventario
{
    public interface IEncantamento : IEfeito
    {
        public string Nome { get; set; }
        string Descricao { get; }
    }
}
