using NoteQuest.Domain.Core.Interfaces.Inventario;

namespace NoteQuest.Domain.ItensContext.ObjectValue.Tesouros
{
    public class PocaoDeCura : IItem
    {
        private const string DefaultNome = "Poção de Cura";
        private const string DefaultDescricao = "Recupera todos os pontos de vida";
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int? Pv { get; set; }

        public PocaoDeCura(string nome = DefaultNome, string descricao = DefaultDescricao, int? pv = null)
        {
            Nome = nome;
            Descricao = descricao;
            Pv = pv;
        }
    }
}
