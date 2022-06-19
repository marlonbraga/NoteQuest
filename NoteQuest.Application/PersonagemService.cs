using NoteQuest.Application.Interfaces;
using NoteQuest.Domain.Core.Interfaces;

namespace NoteQuest.Application
{
    public class PersonagemService : IPersonagemService
    {
        public IPersonagemBuilder PersonagemBuilder { get; set; }

        public PersonagemService(IPersonagemBuilder personagemBuilder)
        {
            PersonagemBuilder = personagemBuilder;
        }

        public IPersonagem CriarPersonagem(string nome, int indiceRaca, int indiceClasse)
        {
            IPersonagem personagem = PersonagemBuilder.BuildPersonagem(nome, indiceRaca, indiceClasse);
            return personagem;
        }

        public IPersonagem CriarPersonagem()
        {
            IPersonagem personagem = PersonagemBuilder.BuildPersonagem();
            return personagem;
        }

        public IPersonagem NomearPersonagem(IPersonagem personagem, string nome)
        {
            personagem = PersonagemBuilder.DecorateNome(personagem, nome);
            return personagem;
        }

        public IPersonagem DefinirRaca(IPersonagem personagem, int indiceRaca)
        {
            personagem = PersonagemBuilder.DecorateRaca(personagem, indiceRaca);
            return personagem;
        }

        public IPersonagem DefinirClasse(IPersonagem personagem, int indiceClasse)
        {
            personagem = PersonagemBuilder.DecorateClasse(personagem, indiceClasse);
            return personagem;
        }
    }
}
