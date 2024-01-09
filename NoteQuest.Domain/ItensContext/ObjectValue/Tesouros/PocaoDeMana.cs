using NoteQuest.Domain.Core.Interfaces.Inventario;

namespace NoteQuest.Domain.ItensContext.ObjectValue.Tesouros
{
    public class PocaoDeMana : IItem
    {
        private const string DefaultNome = "Poção de Mana";
        private const string DefaultDescricao = "Recupera os usos de todas as magias";
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public PocaoDeMana(string nome = DefaultNome, string descricao = DefaultDescricao)
        {
            Nome = nome;
            Descricao = descricao;
        }
    }
}
