using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;

namespace NoteQuest.Domain.Core.ObjectValue
{
    public class Escolha : IEscolha
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public IAcao Acao { get; set; }
        public IPersonagem Personagem { get; set; }

        public Escolha(IAcao acao, IPersonagem personagem)
        {
            Titulo = acao.Titulo;
            Descricao = acao.Descricao;
            Acao = acao;
            Personagem = personagem;
        }

        public ConsequenciaDTO Executar()
        {
            Acao = Personagem.ModificarAcao(Acao);

            return Acao.Executar();
        }
    }
}
