using NoteQuest.Domain.Core.Interfaces.Personagem;
using System;

namespace NoteQuest.Domain.Core.Interfaces
{
    public interface IMagia : IEfeito
    {
        public int Indice { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Type TipoAcao { get; set; }
        public IAcao Acao { get; set; }
    }
}
