namespace NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados
{
    public interface IArmadura : IEquipamento
    {
        public IPontosDeVida Pv { get; set; }
    }
}
