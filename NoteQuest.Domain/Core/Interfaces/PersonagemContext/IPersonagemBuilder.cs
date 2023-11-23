using NoteQuest.Domain.Core.Interfaces.PersonagemContext.Data;

namespace NoteQuest.Domain.Core.Interfaces.PersonagemContext
{
    public interface IPersonagemBuilder
    {
        public IRacaRepository RacaRepository { get; set; }
        public IClasseRepository ClasseRepository { get; set; }
        public IPersonagem BuildPersonagem();
        public IPersonagem BuildPersonagem(string nome, int indiceRaca, int indiceClasse);
        public IPersonagem DecorateNome(IPersonagem personagem, string nome);
        public IPersonagem DecorateRaca(IPersonagem personagem, int indiceRaca);
        public IPersonagem DecorateClasse(IPersonagem personagem, int indiceClasse);
    }
}
