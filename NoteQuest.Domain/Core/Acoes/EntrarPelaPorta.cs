using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces;

namespace NoteQuest.Domain.Core.Acoes
{
    public class EntrarPelaPorta : IAcao
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public IPortaComum Porta { get; set; }

        public EntrarPelaPorta(IPortaComum porta)
        {
            Porta = porta;
            Titulo = $"Entrar pela porta de {porta.Posicao}";
            Descricao = "Acessa nova sala. Se houver monstros, você ataca primeiro.";
        }

        public ConsequenciaDTO Executar()
        {
            return null;
        }
    }
}
