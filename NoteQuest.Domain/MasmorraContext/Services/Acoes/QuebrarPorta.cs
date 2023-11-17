using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class QuebrarPorta : IAcao
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public QuebrarPorta(int indice, IPortaComum porta)
        {
            Titulo = $"Quebrar porta {porta.Posicao}";
            Descricao = "Abre acesso a sala trancada sem gastar tochas. Se houver monstros, sofrerá ataque primeiro.";
        }

        public ConsequenciaDTO Executar(int? indice = null)
        {
            return null;
        }
    }
}
