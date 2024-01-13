namespace NoteQuest.Domain.Core.Interfaces.Inventario
{
    public interface IEquipamento : IItem
    {
        bool EstaAmaldicoado { get; set; }
        void DefinirEncantamento(IEncantamento encantamento);
    }
}
