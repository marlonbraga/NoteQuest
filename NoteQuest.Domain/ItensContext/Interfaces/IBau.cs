using NoteQuest.Domain.Core.Interfaces.Inventario;

namespace NoteQuest.Domain.ItensContext.Interfaces
{
    public interface IBau : IRepositorio
    {
        bool EstaFechado => !EstaAberto;

        bool EstaAberto { get; set; }
    }
}
