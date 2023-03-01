using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using NoteQuest.Domain.Core.Interfaces.Masmorra.Services;
using System;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class AcharPassagemSecretaService : IAcharPassagemSecretaService
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public Func<ConsequenciaDTO> Execucao { get; set; }

        public AcharPassagemSecretaService()
        {
            Titulo = "Procurar passagem secreta";
            Descricao = "Ação demorada. Gasta 1 tocha";
            AcaoTipo = AcaoTipo.Segmento;
        }

        public ConsequenciaDTO Executar()
        {
            return null;
        }
    }
}
