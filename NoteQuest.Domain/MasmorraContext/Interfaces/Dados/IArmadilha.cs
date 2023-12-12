using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces.Personagem;

namespace NoteQuest.Domain.MasmorraContext.Interfaces.Dados
{
    public interface IArmadilha
    {
        public string Descricao { get; set; }
        public int Dano { get; set; }

        string Efeito(IPersonagem personagem);
    }
}
