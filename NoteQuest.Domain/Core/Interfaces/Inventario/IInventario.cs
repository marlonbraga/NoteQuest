using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core.Interfaces.Inventario
{
    public interface IInventario
    {
        ushort Tochas { get; }
        ushort Provisoes { get; }
        ushort Moedas { get; }
        ushort GastarTochas(ushort qtd);
        IList<IItem> Mochila { get; }
        IItensEquipados Equipamentos { get; }

        bool RemoverItem(IItem item);

        bool AdicionaItem(IItem item);

        bool Equipar(IEquipamento equipamento);

        bool Desequipar(IEquipamento equipamento);

        bool UsarItem(IItem item);

        bool EquiparItemDeMao(IItemDeMao equipamento);
    }
}
