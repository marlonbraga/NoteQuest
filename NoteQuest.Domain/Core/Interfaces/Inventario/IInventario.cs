using System.Collections.Generic;

namespace NoteQuest.Domain.Core.Interfaces.Inventario
{
    public interface IInventario
    {
        public ushort Tochas { get; }
        public ushort Provisoes { get; }
        public ushort Moedas { get; }
        public ushort GastarTochas(ushort qtd);
        public IList<IItem> Mochila { get; }
        public IItensEquipados Equipamentos { get; }

        public bool RemoverItem(IItem item);

        public bool AdicionaItem(IItem item);

        public bool Equipar(IEquipamento equipamento);

        public bool Desequipar(IEquipamento equipamento);

        public bool UsarItem(IItem item);
    }
}
