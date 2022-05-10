using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces;

namespace NoteQuest.Domain.Core.Acoes
{
    public class EntrarPelaPorta : IAcao
    {
        public IPortaComum Porta { get; set; }

        public EntrarPelaPorta(IPortaComum porta)
        {
            Porta = porta;
        }

        public ResultadoAcao executar()
        {
            return null;
        }
    }
}
