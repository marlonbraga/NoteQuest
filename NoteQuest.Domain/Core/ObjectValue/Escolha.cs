using NoteQuest.Domain.Core.Interfaces;

namespace NoteQuest.Domain.Core.ObjectValue
{
    public class Escolha : IEscolha
    {
        //public string Titulo { get; set; }
        //public string Descricao { get; set; }
        public IAcao Acao { get; set; }
        
        public Escolha(/*string titulo, string descricao, */IAcao acao)
        {
            //Titulo = titulo;
            //Descricao = descricao;
            Acao = acao;
        }
    }
}
