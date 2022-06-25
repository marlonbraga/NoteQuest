using NoteQuest.Domain.Core.Interfaces.Inventario;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core.Interfaces
{
    public interface IPersonagem
    {
        public IPontosDeVida Pv { get; set; }
        public string Nome { get; set; }
        public IRaca Raca { get; set; }
        public List<IClasse> Classes { get; set; }
        public IInventario Inventario { get; set; }

        public IAcao ModificarAcao(IAcao acao);

        public void Build(string nome, IRaca indiceRaca, IClasse indiceClasse);
    }
}
