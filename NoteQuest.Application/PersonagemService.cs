using NoteQuest.Domain.Core.Interfaces.Inventario;
using NoteQuest.Domain.Core.Interfaces.Inventario.ItensEquipados;
using NoteQuest.Domain.Core.Interfaces.Personagem;

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

        public bool Equipar(IPersonagem personagem, IItem equipamento)
        {
            if (equipamento is IItemDeMao)
                return personagem.Inventario.EquiparItemDeMao((IItemDeMao)equipamento);
            if (equipamento is IEquipamento)
                return personagem.Inventario.Equipar((IItemDeMao)equipamento);

            return false;
        }
        
        public bool Desequipar(IPersonagem personagem, IEquipamento equipamento)
        {
            return personagem.Inventario.Desequipar(equipamento);
        }
    }
}
