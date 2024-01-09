namespace NoteQuest.Domain.Core.Interfaces.Inventario
{
    public interface IEquipamento : IItem
    {
        void DefinirEncantamento(IEncantamento encantamento);
    }
}
