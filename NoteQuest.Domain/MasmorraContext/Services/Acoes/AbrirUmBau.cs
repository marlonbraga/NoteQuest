using NoteQuest.Domain.Core.DTO;
using NoteQuest.Domain.Core.Interfaces;
using System;

namespace NoteQuest.Domain.MasmorraContext.Services.Acoes
{
    public class AbrirUmBau : IAcao
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public AcaoTipo AcaoTipo { get; set; }
        public GatilhoDeAcao GatilhoDeAcao { get; set; }
        public Func<ConsequenciaDTO> Execucao { get; set; }

        public AbrirUmBau()
        {
            Titulo = "Abrir Baú";
            Descricao = "Encontra moedas e tesouros; Raramente aciona armadilhas.";
        }

        public ConsequenciaDTO Executar(int? indice = null)
        {
            return null;
        }
    }
}
