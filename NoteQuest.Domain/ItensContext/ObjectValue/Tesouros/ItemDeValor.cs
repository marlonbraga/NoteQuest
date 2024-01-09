using NoteQuest.Domain.Core.Interfaces.Inventario;

namespace NoteQuest.Domain.ItensContext.ObjectValue.Tesouros
{
    public class ItemDeValor : IItem
    {
        private const string DefaultDescricao = "Vale algumas moedas na cidade";
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Valor { get; set; }

        public ItemDeValor(string nome, string descricao = DefaultDescricao, int valor = 1)
        {
            Nome = nome;
            Descricao = descricao;
            Valor = valor ;
        }
    }
}
