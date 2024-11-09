using NoteQuest.Domain.Core.Interfaces.Inventario;
using NoteQuest.Domain.Core.Interfaces.Personagem;

namespace NoteQuest.Application
{
    public interface IPersonagemService
    {
        IPersonagem CriarPersonagem();
        IPersonagem CriarPersonagem(string nome, int indiceRaca, int indiceClasse);
        IPersonagem NomearPersonagem(IPersonagem personagem, string nome);
        IPersonagem DefinirRaca(IPersonagem personagem, int indiceRaca);
        IPersonagem DefinirClasse(IPersonagem personagem, int indiceClasse);
        bool Equipar(IPersonagem personagem, IItem equipamento);
        bool Desequipar(IPersonagem personagem, IEquipamento equipamento);
    }
}
