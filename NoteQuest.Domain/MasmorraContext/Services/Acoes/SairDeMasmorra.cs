using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class SairDeMasmorra : IAcao
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public SairDeMasmorra()
        {
            Titulo = "Sair de Masmorra";
            Descricao = "Pode voltar a cidade para se recuperar. Mas os montros da masmorra também restaurarão as energias.";
        }

        public ConsequenciaDTO Executar(int? indice = null)
        {
            return null;
        }
    }
}
