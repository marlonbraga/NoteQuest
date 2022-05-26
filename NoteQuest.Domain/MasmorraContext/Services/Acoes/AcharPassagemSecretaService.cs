using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.MasmorraContext.Interfaces.Services;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class AcharPassagemSecretaService : IAcharPassagemSecretaService
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public AcharPassagemSecretaService()
        {
            Titulo = "Procurar passagem secreta";
            Descricao = "Ação demorada. Gasta 1 tocha";
        }

        public ConsequenciaDTO Executar()
        {
            return null;
        }
    }
}
