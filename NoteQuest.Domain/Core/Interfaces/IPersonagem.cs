using System.Collections.Generic;

namespace NoteQuest.Domain.Core.Interfaces
{
    public interface IPersonagem
    {
        public IPontosDeVida Pv { get; set; }
        public string Nome { get; set; }
        public IRaca Raca { get; set; }
        public List<IClasse> Classes { get; set; }

        public IAcao ModificarAcao(IAcao acao);
    }
}
