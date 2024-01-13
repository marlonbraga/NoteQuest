namespace NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados
{
    public interface IArma : IItemDeMao
    {
        public bool EmpunhaduraDupla { get; set; }
        public short Dano { get; set; }
        public void Build(string nome, short dano = 0, bool empunhaduraDupla = false, bool maldicao = false);
    }
}
