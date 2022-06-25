using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core.Interfaces.Inventario
{
    public interface IItensEquipados
    {
        public IItemDeMao MaoDireita { get; set; }
        public IItemDeMao MaoEsquerda { get; set; }
        public IPeitoral Peitoral { get; set; }
        public IElmo Elmo { get; set; }
        public IBotas Botas { get; set; }
        public IBraceletes Braceletes { get; set; }
        public IOmbreiras Ombreiras { get; set; }
        public IList<IAmuleto> Amuletos { get; set; }

        public IList<IEquipamento> Listar();
    }
}
