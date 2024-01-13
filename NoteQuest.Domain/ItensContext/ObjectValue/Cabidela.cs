using NoteQuest.Domain.Core.Interfaces.Inventario;

namespace NoteQuest.Domain.ItensContext.ObjectValue
{
    public class Cabidela : IItem
    {
        public int Qtd { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public Cabidela(int qtd)
        {
            Qtd = qtd;
            Nome = qtd == 1 ? $"1 moeda" : $"{qtd} moedas";
            Descricao = $"Compra coisas na cidade";
        }
    }
}
