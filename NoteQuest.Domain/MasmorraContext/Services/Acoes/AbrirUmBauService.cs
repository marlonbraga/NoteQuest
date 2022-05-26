using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Services;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class AbrirUmBauService : IAbrirUmBauService
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public AbrirUmBauService()
        {
            Titulo = "Abrir Baú";
            Descricao = "Encontra moedas e tesouros; Raramente aciona armadilhas.";
        }

        public ConsequenciaDTO Executar()
        {
            return null;
        }
    }
}
