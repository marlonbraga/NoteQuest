using NoteQuest.Domain.Core.Interfaces.Inventario;
using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;
using NoteQuest.Domain.MasmorraContext.Interfaces;

namespace NoteQuest.Domain.ItensContext.Interfaces
{
    public interface IItemFactory
    {
        IItem GeraTesouro(IMasmorra masmorra, int? indice = null, int? indice2 = null);
        
        IItem GeraMaravilha(int? indice = null);
        
        IItem GeraItemMagico(int? indice = null, int? indice2 = null);
        
        IEquipamento GeraArmadura(IEncantamento encantamento, int? indice = null);
        
        IArma GeraArma(IEncantamento encantamento, int? indice = null);
    }
}
