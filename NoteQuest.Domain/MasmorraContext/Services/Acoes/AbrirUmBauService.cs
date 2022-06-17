using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.MasmorraContext.Interfaces.Services;
using System;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class AbrirUmBauService : IAbrirUmBauService
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Func<ConsequenciaDTO> Execucao { get; set; }

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
