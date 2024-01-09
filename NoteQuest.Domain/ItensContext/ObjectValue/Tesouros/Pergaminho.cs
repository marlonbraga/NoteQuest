using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Inventario;

namespace NoteQuest.Domain.ItensContext.ObjectValue.Tesouros
{
    public class Pergaminho : IItem
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public IMagia Magia { get; set; }

        public Pergaminho(string nome, string descricao, IMagia magia)
        {
            Nome = nome;
            Descricao = descricao;
            Magia = magia;
        }
    }
}
