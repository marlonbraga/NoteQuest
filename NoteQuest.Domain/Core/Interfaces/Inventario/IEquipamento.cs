namespace NoteQuest.Domain.Core.Interfaces.Inventario
{
    public interface IEquipamento : IItem
    {
        IPontosDeVida Pv { get; set; }
    }
}
