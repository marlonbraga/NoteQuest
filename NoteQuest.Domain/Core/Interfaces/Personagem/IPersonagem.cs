using NoteQuest.Domain.Core.Interfaces.Inventario;
using System.Collections.Generic;

namespace NoteQuest.Domain.Core.Interfaces.Personagem
{
    public interface IPersonagem
    {
        IPontosDeVida Pv { get; set; }
        string Nome { get; set; }
        IRaca Raca { get; set; }
        List<IClasse> Classes { get; set; }
        IInventario Inventario { get; set; }

        IAcao ChainOfResponsabilityEfeito(IAcao acao);
        void Build(string nome, IRaca indiceRaca, IClasse indiceClasse);
    }
}